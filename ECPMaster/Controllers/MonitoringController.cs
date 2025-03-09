using System.Threading.Tasks;
using ECPMaster.DbContext;
using ECPMaster.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECPMaster.Controllers
{
    public class MonitoringController : Controller
    {
        private readonly ECPDbContext _dbContext;

        public MonitoringController(ECPDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> NodesMonitoring()
        {
            return View(await NetworkUtils.UpdateNodesStatus(await _dbContext.EcpNodes.ToListAsync()));
        }
    }
}