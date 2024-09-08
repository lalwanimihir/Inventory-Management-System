using static Inventory_Management_System.Shared.Enums.CategoryEnum;

namespace Inventory_Management_System.Application.Dtos.InventoryDto.ResponseDto
{
    public class GetProductsResponseDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public string? DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
