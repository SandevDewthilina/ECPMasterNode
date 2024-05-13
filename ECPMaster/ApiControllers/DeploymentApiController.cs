using System;
using System.Linq;
using System.Threading.Tasks;
using ECPMaster.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECPMaster.APIControllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    public class DeploymentApiController : Controller
    {
        private readonly ECPDbContext _dbContext;

        public DeploymentApiController(ECPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*
         * offset: result exclude offset index [start from 0]
         */
        [HttpGet]
        public IActionResult GetLogs(int deploymentId, int offset)
        {
            return Json(_dbContext.EcpLogs
                .Where(log => log.DeploymentId == deploymentId && log.Id > offset)
                .OrderBy(log => log.Id)
                .Select(log => new { line = $"{log.Time:s} {log.Line}", id = log.Id }));
        }
    }
}