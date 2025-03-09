using System.Threading.Tasks;
using ECPMaster.DbContext;
using ECPMaster.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECPMaster.Controllers
{
    public class ArtifactsController : Controller
    {
        private readonly ECPDbContext _dbContext;

        public ArtifactsController(ECPDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> ListArtifacts()
        {
            return View(await _dbContext.EcpArtifacts.ToListAsync());
        }

        public IActionResult AddArtifacts()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddArtifacts(ECPArtifact artifact)
        {
            await _dbContext.EcpArtifacts.AddAsync(artifact);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("ListArtifacts");
        }
    }
}