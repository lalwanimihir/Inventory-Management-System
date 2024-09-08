using Inventory_Management_System.Domain.Entities.IdentityDbEntity;
using Inventory_Management_System.Domain.Entities.InventoryDbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Infrastructure.Data
{
    public static class ModalBuilderExtensionMethod
    {
        public static void ConfigureModalBuilder(this ModelBuilder builder)
        {
            var UserRoleId = "2a7a18e6-4016-42da-b310-fcfca9dca1f9";
            var AdminRoleId = "d83da4d4-6b63-49df-aa32-4548c240f381";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = UserRoleId,
                    ConcurrencyStamp = UserRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                new IdentityRole
                {
                    Id = AdminRoleId,
                    ConcurrencyStamp = AdminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "53434386-27b6-44ad-b5df-60f2330a0f76",
                    UserName = "user1",
                    NormalizedUserName = "USER1",
                    Email = "user1@gmail.com",
                    NormalizedEmail = "user1@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "User1@123")
                },
                new ApplicationUser
                {
                    Id = "b3e40240-48f5-4e68-b371-3d628468993f",
                    UserName = "user2",
                    NormalizedUserName = "USER2",
                    NormalizedEmail = "user2@gmail.com",
                    Email = "user2@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "User2@123")
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = AdminRoleId,
                    UserId = "53434386-27b6-44ad-b5df-60f2330a0f76"
                },
                new IdentityUserRole<string>
                {
                    RoleId = UserRoleId,
                    UserId = "b3e40240-48f5-4e68-b371-3d628468993f"
                }
            );

            builder.Entity<ProductDetails>().HasQueryFilter(p => !p.IsDeleted);

            builder.Entity<InventoryRequestDetails>(entity =>
            {
                entity.ToTable("InventoryRequestDetails");
                entity.HasOne(c => c.ProductDetails).WithMany(e => e.InventoryRequestDetails).HasForeignKey(c => c.ProductId);
            });
        }
    }
}
