namespace Inventory_Management_System.Application.Dtos.InventoryDto.ResponseDto
{
    public class InventoryPaginationResponse
    {
        public int TotalCount { get; set; }
        public List<GetInventoryRequestsResponseDto> InventoryRequests { get; set; }
        public bool IsSucceeded { get; set; }
    }
}
