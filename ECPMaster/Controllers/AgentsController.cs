using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECPMaster.Controllers
{
    [Authorize]
    public class AgentsController : Controller
    {
        public IActionResult ViewAgents()
        {
            
            return View();
        }

        public IActionResult ConfigureAgent()
        {
            return View();
        }

        public IActionResult EditSlaveNodes()
        {
            return View();
        }
    }
}