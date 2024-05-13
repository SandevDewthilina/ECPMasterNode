using System.Net.NetworkInformation;
using System.Threading.Tasks;
using ECPMaster.DbContext;
using ECPMaster.Entities;
using ECPMaster.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECPMaster.Controllers
{
    [Authorize]
    public class NodesController : Controller
    {
        private readonly ECPDbContext _dbContext;

        public NodesController(ECPDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IActionResult> ViewAgents()
        {
            var nodes = await _dbContext.EcpNodes.ToListAsync();

            nodes = await NetworkUtils.UpdateNodesStatus(nodes);

            _dbContext.EcpNodes.UpdateRange(nodes);
            await _dbContext.SaveChangesAsync();
            return View(nodes);
        }

        public IActionResult ConfigureAgent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfigureAgent(ECPNode node)
        {
            node.NodeState = AgentState.Down;
            await _dbContext.EcpNodes.AddAsync(node);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("ViewAgents");
        }

        public IActionResult EditSlaveNodes()
        {
            return View();
        }
    }
}