namespace Inventory_Management_System.Application.Dtos.InventoryDto.RequestDto
{
    public class AddInventoryRequestDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string Reason { get; set; }
    }
}
