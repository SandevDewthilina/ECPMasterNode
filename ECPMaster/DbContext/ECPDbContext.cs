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

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //
        //     // set primary key as userRoleId + TagId
        //     modelBuilder.Entity<UserRoleTagMapping>()
        //         .HasKey(c => new {c.UserRoleId, c.TagId});
        //     // set primary key as VehicleTagId + ContainerTagId
        //     modelBuilder.Entity<ContainerVehicleMapping>()
        //         .HasKey(c => new {c.VehicleAssetId, c.ContainerAssetId});
        // }
        //get the dbset for the entity
        // public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        // {
        //     return Set<TEntity>();
        // }
    }
}