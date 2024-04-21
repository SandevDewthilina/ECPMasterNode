using System.Threading.Tasks;
using ECPMaster.Ansible;
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

        [AllowAnonymous]
        public async Task<IActionResult> RunCommand(string command)
        {
            return Json(await _ansibleRepository.RunCommand(command));
        }
    }
}