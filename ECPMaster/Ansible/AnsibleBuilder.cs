using System.Collections.Generic;
using ECPMaster.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ECPMaster.Ansible
{
    public class AnsibleBuilder
    {
        private static AnsiblePlaybook _playbook;

        private AnsibleBuilder(AnsiblePlaybook playbook)
        {
            _playbook = playbook;
        }

        public static AnsibleBuilder BuildAnsiblePlaybook(
            string name, string hosts, bool become = false, string becomeUser = "ecp")
        {
            _playbook = new AnsiblePlaybook()
            {
                Content = new List<Dictionary<string, object>>() { }
            };
            _playbook.Content.Add(
                new Dictionary<string, object>
                {
                    { "name", name },
                    { "hosts", hosts }
                });
            if (!become) return new AnsibleBuilder(_playbook);

            _playbook.Content[0].Add("become", true);
            _playbook.Content[0].Add("become_user", becomeUser);

            return new AnsibleBuilder(_playbook);
        }

        public AnsibleBuilder AddCleanDirectoryTask(string taskName, string path)
        {
            GetTaskList()
                .Add(new Dictionary<string, object>
                {
                    { "name", taskName },
                    {
                        "ansible.builtin.file",
                        new BuiltInFileModule(path, State.absent, mode: "")
                    }
                });

            return new AnsibleBuilder(_playbook);
        }
        
        public AnsibleBuilder AddCreateDirectoryTask(string taskName, string path)
        {
            GetTaskList()
                .Add(new Dictionary<string, object>
                {
                    { "name", taskName },
                    {
                        "ansible.builtin.file",
                        new BuiltInFileModule(path, State.directory, mode: "0755")
                    }
                });

            return new AnsibleBuilder(_playbook);
        }

        public AnsibleBuilder AddDownloadFileTask(string taskName, string url, string dest)
        {
            GetTaskList()
                .Add(new Dictionary<string, object>
                {
                    { "name", taskName },
                    {
                        "get_url",
                        new GetUrlModule(url, dest)
                    }
                });

            return new AnsibleBuilder(_playbook);
        }

        public AnsibleBuilder AddServiceTask(string taskName, string name, State state)
        {
            GetTaskList()
                .Add(new Dictionary<string, object>
                {
                    { "name", taskName },
                    {
                        "service",
                        new ServiceModule(name, state)
                    }
                });

            return new AnsibleBuilder(_playbook);
        }

        public AnsibleBuilder AddDockerContainerTask(string taskName, string name, string image, State state,
            List<string> ports = null, List<string> volumes = null, string restartPolicy = "no", Dictionary<string, string> etcHosts = null)
        {
            GetTaskList()
                .Add(new Dictionary<string, object>
                {
                    { "name", taskName },
                    {
                        "docker_container",
                        new DockerContainerModule(name, image, state, ports, volumes, restartPolicy, etcHosts)
                    }
                });

            return new AnsibleBuilder(_playbook);
        }

        public AnsibleBuilder AddCommandTask(string taskName, string command, bool ignoreErrors = false)
        {
            var dic = new Dictionary<string, object>
            {
                { "name", taskName },
                {
                    "command",
                    command
                }
            };

            if (ignoreErrors)
            {
                dic.Add("ignore_errors", "yes");
            }
            
            GetTaskList()
                .Add(dic);

            return new AnsibleBuilder(_playbook);
        }

        public AnsibleBuilder AddMoveFileTask(string taskName, string src, string dest)
        {
            GetTaskList()
                .Add(new Dictionary<string, object>
                {
                    { "name", taskName },
                    {
                        "template",
                        new TemplateModule(src, dest)
                    }
                });

            return new AnsibleBuilder(_playbook);
        }

        public AnsibleBuilder AddDockerLoginTask(string taskName, string username, string password)
        {
            GetTaskList()
                .Add(new Dictionary<string, object>
                {
                    { "name", taskName },
                    {
                        "docker_login",
                        new DockerLoginModule(username, password)
                    }
                });

            return new AnsibleBuilder(_playbook);
        }

        public AnsibleBuilder AddDockerBuildAndPushTask(string taskName, string path, string name, string tag,
            bool push)
        {
            GetTaskList()
                .Add(new Dictionary<string, object>
                {
                    { "name", taskName },
                    {
                        "community.docker.docker_image",
                        new DockerImageModule(path, name, tag, push)
                    }
                });

            return new AnsibleBuilder(_playbook);
        }

        public AnsiblePlaybook Build()
        {
            return _playbook;
        }

        private static List<Dictionary<string, object>> GetTaskList()
        {
            var value = _playbook.Content[0]
                .GetValueOrDefault("tasks", null);

            if (value != null) return value;

            _playbook.Content[0].Add("tasks", new List<Dictionary<string, object>>());
            return _playbook.Content[0]["tasks"];
        }
    }
}