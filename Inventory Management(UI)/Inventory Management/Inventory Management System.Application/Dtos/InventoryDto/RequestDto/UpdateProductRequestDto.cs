using static Inventory_Management_System.Shared.Enums.CategoryEnum;

namespace Inventory_Management_System.Application.Dtos.InventoryDto.RequestDto
{
    public class UpdateProductRequestDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
