using ECPMaster.Entities;
using ECPMaster.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECPMaster.DbContext
{
    public class ECPDbContext : IdentityDbContext<ApplicationUser>
    {
        public ECPDbContext(DbContextOptions<ECPDbContext> options) : base(options)
        {
        }
        // Entities
        public DbSet<ECPNode> EcpNodes{ get; set; }
        public DbSet<ECPDeployment> EcpDeployments { get; set; }
        public DbSet<ECPArtifact> EcpArtifacts { get; set; }
        public DbSet<ECPDbConfig> EcpDbConfigs { get; set; }
        public DbSet<ECPLog> EcpLogs { get; set; }
    }
}