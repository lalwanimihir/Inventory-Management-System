using static Inventory_Management_System.Shared.Enums.StatusEnum;

namespace Inventory_Management_System.Application.Dtos.InventoryDto.RequestDto
{
    public class UpdateInventoryRequestDto
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}
