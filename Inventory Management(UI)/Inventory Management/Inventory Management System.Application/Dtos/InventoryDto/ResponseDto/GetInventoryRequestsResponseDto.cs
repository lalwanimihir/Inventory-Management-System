using Inventory_Management_System.Domain.Entities.IdentityDbEntity;
using static Inventory_Management_System.Shared.Enums.StatusEnum;

namespace Inventory_Management_System.Application.Dtos.InventoryDto.ResponseDto
{
    public class GetInventoryRequestsResponseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string UserId { get; set; }
        public string ProductName { get; set; }
        public string Reason { get; set; }
        public Status? Status { get; set; }
    }
}
