using System.Threading.Tasks;
using ECPMaster.AnsibleCLI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECPMaster.Controllers
{
    [Authorize]
    public class AgentsController : Controller
    {
        private readonly IAnsibleRepository _ansibleRepository;

        public AgentsController(IAnsibleRepository ansibleRepository)
        {
            _ansibleRepository = ansibleRepository;
        }
        public async Task<IActionResult> ViewAgents()
        {
            var results= Json(await _ansibleRepository.GetAgentList());
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