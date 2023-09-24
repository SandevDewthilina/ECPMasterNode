using Microsoft.AspNetCore.Mvc;

namespace ECPMaster.Controllers
{
    public class MonitoringController : Controller
    {
        public IActionResult NodesMonitoring()
        {
            return View();
        }
    }
}