using Inventory_Management_System.Application.Dtos.InventoryDto.ResponseDto;
using MediatR;

namespace Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Queries.GetAll
{
    public class GetAllInventoryRequestsQuery:IRequest<InventoryPaginationResponse>
    {
        public string? FilterOn { get; set; }
        public string? FilterQuery { get; set; }
        public string? SortBy { get; set; }
        public bool IsAscending { get; set; } = true;
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 1000;
    }
}
