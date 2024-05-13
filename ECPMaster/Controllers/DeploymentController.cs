using System;
using System.Linq;
using System.Threading.Tasks;
using ECPMaster.AsyncDataServices;
using ECPMaster.DbContext;
using ECPMaster.Entities;
using ECPMaster.Utils;
using ECPMaster.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECPMaster.Controllers
{
    public class DeploymentController : Controller
    {
        private readonly ECPDbContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DeploymentController(ECPDbContext dbContext, IWebHostEnvironment hostEnvironment, IServiceScopeFactory serviceScopeFactory)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<IActionResult> DeploymentPipeline()
        {
            return View(new DeploymentViewModel()
            {
                Artifacts = await _dbContext.EcpArtifacts.ToListAsync(),
                Nodes = await NetworkUtils.UpdateNodesStatus(await _dbContext.EcpNodes.ToListAsync()),
                MySqlPort = 3306,
                MySqlUser = "root",
                SystemPort = 18001
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeploymentPipeline(DeploymentViewModel model)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var artifact =
                    await _dbContext.EcpArtifacts.FirstOrDefaultAsync(a => a.Name.Equals(model.ArtifactName));

                var dbConfig = new ECPDbConfig()
                {
                    User = model.MySqlUser,
                    Password = model.MySqlPassword,
                    DbBackupFileName = model.DbBackupFileName,
                    Port = model.MySqlPort
                };

                await _dbContext.EcpDbConfigs.AddAsync(dbConfig);
                await _dbContext.SaveChangesAsync();

                var deployment = new ECPDeployment()
                {
                    Name = model.SystemName,
                    Port = model.SystemPort,
                    SubDomain = model.SubDomain,
                    Type = DeploymentType.Contained,
                    Status = DeploymentStatus.New,
                    NodeId = model.NodeId,
                    DbConfigId = dbConfig.Id,
                    ArtifactId = artifact.Id
                };
                await _dbContext.EcpDeployments.AddAsync(deployment);

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                
                var jobExecutor = new JobExecutor(_serviceScopeFactory);
                jobExecutor.ExecuteDeployment(await _dbContext
                    .EcpDeployments
                    .Include(d => d.Node)
                    .Include(d => d.Artifact)
                    .Include(d => d.DbConfig)
                    .FirstOrDefaultAsync(d => d.Id == deployment.Id));
                
                return RedirectToAction("ExecuteDeployment", new {deploymentId = deployment.Id});
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

            return RedirectToAction("DeploymentPipeline");
        }

        public IActionResult ExecuteDeployment()
        {
            return View();
        }

        public async Task<IActionResult> ManageDeployments()
        {
            return View(await _dbContext.EcpDeployments
                .Include(d => d.Artifact)
                .Include(d => d.Node)
                .Include(d => d.DbConfig).ToListAsync());
        }
    }
}