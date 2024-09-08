using Inventory_Management_System.Application.IUnitOfWork;
using Inventory_Management_System.Shared.Response;
using MediatR;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Queries.GetAll
{
    public class GetAllActiveProductsQueryHandler : IRequestHandler<GetAllActiveProductsQuery, ActiveProductsResponse>
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public GetAllActiveProductsQueryHandler( IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<ActiveProductsResponse> Handle(GetAllActiveProductsQuery request, CancellationToken cancellationToken)
        {
            var response = new ActiveProductsResponse();
            var products = _unitOfWorkRepository.ProductDetailRepository.GetAllAsync();

            products = products.Where(x => x.IsActive == true && x.Quantity >0);

            response.IsSucceeded = true;
            response.ActiveProducts = products.ToList();
            return response;
        }
    }
}
