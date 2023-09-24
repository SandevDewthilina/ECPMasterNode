using ECPMaster.AsyncDataServices;
using Microsoft.AspNetCore.Mvc;

namespace ECPMaster.Controllers
{
    public class DeploymentController : Controller
    {
        private readonly IRabbitMqClient _rabbitMqClient;

        public DeploymentController(IRabbitMqClient rabbitMqClient)
        {
            _rabbitMqClient = rabbitMqClient;
        }
        public IActionResult DeploymentPipeline()
        {
            return View();
        }

        public IActionResult ExecuteDeployment()
        {
            return View();
        }

        public IActionResult PingService2()
        {
            _rabbitMqClient.CreateAJob();
            return Json(new {success = true});
        }
    }
}