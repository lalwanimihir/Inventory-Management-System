using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Inventory_Management_System.Shared.Enums.CategoryEnum;

namespace Inventory_Management_System.Domain.Entities.InventoryDbEntities
{
    public class ProductDetails
    {
        [Key]
        public int Id { get; set; }
        public string ProductName {  get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string? DeletedBy { get; set; }
        public DateTime DeletedDate { get; set;}
        public virtual ICollection<InventoryRequestDetails>? InventoryRequestDetails { get; set; }
    }
}
