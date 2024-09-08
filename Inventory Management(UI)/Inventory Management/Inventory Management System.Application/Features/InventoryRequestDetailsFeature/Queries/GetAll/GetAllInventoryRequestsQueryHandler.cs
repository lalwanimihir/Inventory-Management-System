using AutoMapper;
using Inventory_Management_System.Application.Dtos.InventoryDto.ResponseDto;
using Inventory_Management_System.Application.IUnitOfWork;
using MediatR;

namespace Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Queries.GetAll
{
    public class GetAllInventoryRequestsQueryHandler : IRequestHandler<GetAllInventoryRequestsQuery, InventoryPaginationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public GetAllInventoryRequestsQueryHandler(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<InventoryPaginationResponse> Handle(GetAllInventoryRequestsQuery request, CancellationToken cancellationToken)
        {
            var inventoryRequests = _unitOfWorkRepository.InventoryRequestRepository.GetAllAsync(includeProperties: "ApplicationUser");

            inventoryRequests = inventoryRequests.OrderByDescending(x => x.Id);
            var totalCount = inventoryRequests.Count();

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
