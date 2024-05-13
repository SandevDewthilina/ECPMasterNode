using System.Threading.Tasks;
using ECPMaster.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECPMaster.APIControllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    public class NodesApiController : Controller
    {
        private readonly ECPDbContext _dbContext;

        public NodesApiController(ECPDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetNodes()
        {
            return Json(await _dbContext.EcpNodes.ToListAsync());
        }
    }
}