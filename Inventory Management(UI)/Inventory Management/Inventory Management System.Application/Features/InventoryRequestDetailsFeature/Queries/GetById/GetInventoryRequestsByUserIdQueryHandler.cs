using AutoMapper;
using Inventory_Management_System.Application.Dtos.InventoryDto.ResponseDto;
using Inventory_Management_System.Application.IUnitOfWork;
using MediatR;

namespace Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Queries.GetById
{
    public class GetInventoryRequestsByUserIdQueryHandler : IRequestHandler<GetInventoryRequestsByUserIdQuery, InventoryPaginationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public GetInventoryRequestsByUserIdQueryHandler(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<InventoryPaginationResponse> Handle(GetInventoryRequestsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var inventoryRequests = _unitOfWorkRepository.InventoryRequestRepository.GetAllAsync();

            inventoryRequests = inventoryRequests.Where(x=>x.UserId == request.UserId);

            var totalCount = 0;
            //Searching
            if (!string.IsNullOrWhiteSpace(request.FilterQuery))
            {
                var trimmedFilterQuery = request.FilterQuery.Trim().ToLower();
                inventoryRequests = inventoryRequests.Where(x =>
                    x.Id.Equals(trimmedFilterQuery) ||
                    x.ProductName.ToLower().Contains(trimmedFilterQuery) ||
                    x.Quantity.ToString().Contains(trimmedFilterQuery) || 
                    x.Reason.ToLower().Contains(trimmedFilterQuery)
                );
            }
            totalCount = inventoryRequests.Count();

            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                inventoryRequests = request.SortBy switch
                {
                    string sortBy when sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase) =>
                        request.IsAscending ? inventoryRequests.OrderBy(x => x.Id) : inventoryRequests.OrderByDescending(x => x.Id),

                    string sortBy when sortBy.Equals("ProductName", StringComparison.OrdinalIgnoreCase) =>
                        request.IsAscending ? inventoryRequests.OrderBy(x => x.ProductName) : inventoryRequests.OrderByDescending(x => x.ProductName),

                    string sortBy when sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase) =>
                        request.IsAscending ? inventoryRequests.OrderBy(x => x.Reason) : inventoryRequests.OrderByDescending(x => x.Reason),

                    string sortBy when sortBy.Equals("Quantity", StringComparison.OrdinalIgnoreCase) =>
                        request.IsAscending ? inventoryRequests.OrderBy(x => x.Quantity) : inventoryRequests.OrderByDescending(x => x.Quantity),

                    _ => inventoryRequests
                };
            }
            else
            {
                inventoryRequests = request.IsAscending ? inventoryRequests.OrderBy(x => x.Id) : inventoryRequests.OrderByDescending(x => x.Id);
            }

            var skipResults = (request.PageNo - 1) * request.PageSize;
            var result = inventoryRequests.Skip(skipResults).Take(request.PageSize).ToList();

            var response = _mapper.Map<List<GetInventoryRequestsResponseDto>>(result);

            var inventoryPaginationResponse = new InventoryPaginationResponse
            {
                TotalCount = totalCount,
                InventoryRequests = response,
                IsSucceeded = true
            };
            return inventoryPaginationResponse;
        }
    }
}
