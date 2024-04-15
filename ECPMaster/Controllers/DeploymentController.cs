using ECPMaster.AsyncDataServices;
using Microsoft.AspNetCore.Mvc;

namespace ECPMaster.Controllers
{
    public class DeploymentController : Controller
    {
        public IActionResult DeploymentPipeline()
        {
            return View();
        }

        public IActionResult ExecuteDeployment()
        {
            return View();
        }
    }
}