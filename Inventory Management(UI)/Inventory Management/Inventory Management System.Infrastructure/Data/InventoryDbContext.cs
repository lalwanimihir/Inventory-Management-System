using Inventory_Management_System.Domain.Entities.IdentityDbEntity;
using Inventory_Management_System.Domain.Entities.InventoryDbEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Infrastructure.Data
{
    public class InventoryDbContext:IdentityDbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<InventoryRequestDetails> InventoryRequestDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureModalBuilder();
        }
    }
}
 