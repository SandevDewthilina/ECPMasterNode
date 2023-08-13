using Microsoft.AspNetCore.Mvc;

namespace ECPMaster.Controllers
{
    public class ArtifactsController : Controller
    {
        public IActionResult ListArtifacts()
        {
            return View();
        }
    }
}