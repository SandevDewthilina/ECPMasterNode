using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECPMaster.Controllers
{
    [Authorize]
    public class Agents : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}