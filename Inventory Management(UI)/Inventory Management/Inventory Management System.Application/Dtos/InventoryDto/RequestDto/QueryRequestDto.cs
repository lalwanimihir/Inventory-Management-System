namespace Inventory_Management_System.Application.Dtos.InventoryDto.RequestDto
{
    public class QueryRequestDto
    {
        public string? FilterOn { get; set; }
        public string? FilterQuery { get; set; }
        public string? SortBy { get; set; }
        public bool? IsAscending { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 1000;
    }
}
