using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using ECPMaster.Ansible;
using ECPMaster.DbContext;
using ECPMaster.Entities;
using ECPMaster.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ECPMaster.Utils
{
    public static class EcpStateManager
    {
        public static AppState AppState { get; set; }
    }

    public static class NetworkUtils
    {
        static async Task<IPStatus> PingHost(string ipAddress)
        {
            using var ping = new Ping();
            try
            {
                PingReply reply = await ping.SendPingAsync(ipAddress);

                // Unknown = -1, // 0xFFFFFFFF
                // Success = 0,
                // DestinationNetworkUnreachable = 11002, // 0x00002AFA
                // DestinationHostUnreachable = 11003, // 0x00002AFB
                // DestinationProhibited = 11004, // 0x00002AFC
                // DestinationProtocolUnreachable = 11004, // 0x00002AFC
                // DestinationPortUnreachable = 11005, // 0x00002AFD
                // NoResources = 11006, // 0x00002AFE
                // BadOption = 11007, // 0x00002AFF
                // HardwareError = 11008, // 0x00002B00
                // PacketTooBig = 11009, // 0x00002B01
                // TimedOut = 11010, // 0x00002B02
                // BadRoute = 11012, // 0x00002B04
                // TtlExpired = 11013, // 0x00002B05
                // TtlReassemblyTimeExceeded = 11014, // 0x00002B06
                // ParameterProblem = 11015, // 0x00002B07
                // SourceQuench = 11016, // 0x00002B08
                // BadDestination = 11018, // 0x00002B0A
                // DestinationUnreachable = 11040, // 0x00002B20
                // TimeExceeded = 11041, // 0x00002B21
                // BadHeader = 11042, // 0x00002B22
                // UnrecognizedNextHeader = 11043, // 0x00002B23
                // IcmpError = 11044, // 0x00002B24
                // DestinationScopeMismatch = 11045, // 0x00002B25
                return reply.Status;
            }
            catch (PingException)
            {
                // Handle ping exceptions, such as network unreachable or timeout
                return IPStatus.Unknown;
            }
        }

        public static async Task<List<ECPNode>> UpdateNodesStatus(List<ECPNode> nodes)
        {
            foreach (var node in nodes)
            {
                var status = await NetworkUtils.PingHost(node.IPv4);
                node.NodeState = status == IPStatus.Success ? AgentState.Available : AgentState.Down;
            }

            return nodes;
        }
    }

    public class JobExecutor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public JobExecutor(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void ExecuteDeployment(ECPDeployment deployment)
        {
            var workSpaceDir = Path.Combine("home", "ecp", $"ws{deployment.Id}");
            var artifactsDir = Path.Combine(workSpaceDir, "downloaded_file");
            var dockerRegistry = "sandevdewthilina/ecp-core";
            var dockerImageTag = $"d{deployment.Id}-a{deployment.Artifact.Id}-at{deployment.Artifact.Tag}";
            var task = Task.Run(() =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ECPDbContext>();
                var hostingEnv = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

                var rootDir = hostingEnv.ContentRootPath;

                try
                {
                    // Update job status to 'running' in the database
                    UpdateJobStatus(deployment, DeploymentStatus.Pending, db);

                    var playbook = AnsibleBuilder
                        .BuildAnsiblePlaybook(name: deployment.Name, hosts: deployment.Node.NodeIdentifier,
                            become: true, becomeUser: "ecp")
                        .AddCleanDirectoryTask("Clean Workspace", workSpaceDir)
                        .AddCreateDirectoryTask("Create a new workspace", workSpaceDir)
                        .AddDownloadFileTask("Download Artifacts", deployment.Artifact.Url,
                            Path.Combine(workSpaceDir, "downloaded_file.zip"))
                        .AddCommandTask("Extract artifacts",
                            $"unzip {Path.Combine(workSpaceDir, "downloaded_file.zip")} -d {artifactsDir}")
                        .AddMoveFileTask("Create Dockerfile",
                            Path.Combine(rootDir, "ContainedSystemTemplate", "Dockerfile"),
                            Path.Combine(artifactsDir, "Dockerfile"))
                        .AddServiceTask("Ensure docker is running", "docker", State.started)
                        .AddDockerLoginTask("Docker login", "sandevdewthilina2000@gmail.com", "sandevdsic123")
                        .AddDockerBuildAndPushTask("Build and push docker image", artifactsDir, dockerRegistry,
                            dockerImageTag, true)
                        .AddCommandTask("Create DB",
                            $"docker exec -i mysql-core mysql -u {deployment.DbConfig.User} --password={deployment.DbConfig.Password} -e \"CREATE DATABASE IF NOT EXISTS {"cherish_v3"}\"")
                        .AddCommandTask("Copy backup to mysql docker",
                            $"docker cp {Path.Combine(artifactsDir, "artifacts", deployment.DbConfig.DbBackupFileName)} mysql-core:/tmp/{deployment.DbConfig.DbBackupFileName}")
                        .AddCommandTask("Restore Database",
                            $"docker exec -i mysql-core mysql -u {deployment.DbConfig.User} --password={deployment.DbConfig.Password} -e \"use {"cherish_v3"}; source /tmp/{deployment.DbConfig.DbBackupFileName}\"",
                            ignoreErrors: true)
                        .AddDockerContainerTask("Run Web Docker Container", $"deployment-{deployment.Id}",
                            dockerRegistry + ":" + dockerImageTag, State.started, ports: new List<string>() { $"{deployment.Port}:6555" },
                            etcHosts: new Dictionary<string, string>() { { "localhost", "host-gateway" } })
                        .AddCleanDirectoryTask("Clean workspace", workSpaceDir)
                        .Build();
                        
                        playbook.SaveFile(Path.Combine(rootDir, "temp", $"{deployment.Id}"))
                        .Wait();

                    // Execute Ansible script
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "ansible-playbook",
                            Arguments =
                                $"-i {rootDir}/Ansible/playbooks/hosts {rootDir}/temp/{deployment.Id}/playbook.yaml",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = false,
                        }
                    };

                    process.Start();
                    
                    db.EcpLogs.AddRange(playbook.ToYaml().Split("\n").Select(line => new ECPLog() {DeploymentId = deployment.Id, Line = line}));
                    db.SaveChanges();

                    // Stream logs in real-time
                    while (!process.StandardOutput.EndOfStream)
                    {
                        db.EcpLogs.Add(new ECPLog()
                            { DeploymentId = deployment.Id, Line = process.StandardOutput.ReadLine() });
                        db.SaveChanges();
                    }

                    // Handle process exit and update job status accordingly
                    UpdateJobStatus(deployment,
                        process.ExitCode == 0 ? DeploymentStatus.Complete : DeploymentStatus.Failed,
                        db);
                    db.EcpLogs.Add(new ECPLog()
                        { DeploymentId = deployment.Id, Line = $"Exited with {process.ExitCode}" });
                    db.EcpLogs.Add(new ECPLog()
                        { DeploymentId = deployment.Id, Line = $"Deployment {deployment.Status}" });
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    db.EcpLogs.Add(new ECPLog()
                        { DeploymentId = deployment.Id, Line = $"Exception: {e.Message}" });
                    db.SaveChanges();
                }
            });
        }

        private void UpdateJobStatus(ECPDeployment deployment, DeploymentStatus status, ECPDbContext dbContext)
        {
            deployment.Status = status;
            dbContext.EcpDeployments.Update(deployment);
            dbContext.SaveChanges();
        }
    }
}