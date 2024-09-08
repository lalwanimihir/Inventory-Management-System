using Inventory_Management_System.Application.Dtos.InventoryDto.ResponseDto;
using MediatR;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Queries.GetById
{
    public class GetProductByIdQuery:IRequest<GetProductsResponseDto>
    {
        public int Id { get; set; }
    }
}
