using AutoMapper;
using Inventory_Management_System.Application.Dtos.InventoryDto.ResponseDto;
using Inventory_Management_System.Application.IUnitOfWork;
using MediatR;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Queries.GetAll
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, PaginationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public GetAllProductQueryHandler(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<PaginationResponse> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
       {
            var products = _unitOfWorkRepository.ProductDetailRepository.GetAllAsync();
            var totalCount = 0;
            //Searching
            if (!string.IsNullOrWhiteSpace(request.FilterQuery))
            {
                var trimmedFilterQuery = request.FilterQuery.Trim().ToLower();

                products = products.Where(x =>
                    x.Id.Equals(trimmedFilterQuery) ||
                    x.ProductName.ToLower().Contains(trimmedFilterQuery) ||
                    x.Price.ToString().Contains(trimmedFilterQuery) ||
                    x.Quantity.ToString().Contains(trimmedFilterQuery) 
                );
            }
            totalCount = products.Count();

             if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                products = request.SortBy switch
                {
                    string sortBy when sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase) =>
                        request.IsAscending ? products.OrderBy(x => x.Id) : products.OrderByDescending(x => x.Id),

                    string sortBy when sortBy.Equals("ProductName", StringComparison.OrdinalIgnoreCase) =>
                        request.IsAscending ? products.OrderBy(x => x.ProductName) : products.OrderByDescending(x => x.ProductName),

                    string sortBy when sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase) =>
                        request.IsAscending ? products.OrderBy(x => x.Price) : products.OrderByDescending(x => x.Price),

                    string sortBy when sortBy.Equals("Quantity", StringComparison.OrdinalIgnoreCase) =>
                        request.IsAscending ? products.OrderBy(x => x.Quantity) : products.OrderByDescending(x => x.Quantity),

                    _ => products 
                };
            }
            else
            {
                products = request.IsAscending ? products.OrderByDescending(x => x.AddedDate) : products.OrderBy(x => x.AddedDate);
            }

            var skipResults = (request.PageNo - 1) * request.PageSize;
            var result = products.Skip(skipResults).Take(request.PageSize).ToList();

            var response = _mapper.Map<List<GetProductsResponseDto>>(result);

            var paginationResponse = new PaginationResponse
            {
                TotalCount = totalCount,
                Products = response,
                IsSucceeded = true
            };
            return paginationResponse;
        }
    }
}
