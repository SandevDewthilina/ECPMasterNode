using ECPMaster.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECPMaster.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ECPDbContext _db;

        public HomeController(ILogger<HomeController> logger, ECPDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
