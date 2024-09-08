using Inventory_Management_System.Domain.Entities.IdentityDbEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Inventory_Management_System.Shared.Enums.StatusEnum;

namespace Inventory_Management_System.Domain.Entities.InventoryDbEntities
{
    public class InventoryRequestDetails
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ProductDetails")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public string ProductName { get; set; }
        public string Reason { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public Status? Status { get; set; } = Shared.Enums.StatusEnum.Status.InProcess;
        public virtual ProductDetails ProductDetails { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
