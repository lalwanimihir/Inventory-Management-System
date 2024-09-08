namespace Inventory_Management_System.Application.Dtos.InventoryDto.ResponseDto
{
    public class PaginationResponse
    {
        public int TotalCount { get; set; }
        public List<GetProductsResponseDto> Products { get; set; }
        public bool IsSucceeded { get; set; }
    }
}
