using Inventory_Management_System.Shared.Response;
using MediatR;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Queries.GetAll
{
    public class GetAllActiveProductsQuery:IRequest<ActiveProductsResponse>
    {
    }
}
