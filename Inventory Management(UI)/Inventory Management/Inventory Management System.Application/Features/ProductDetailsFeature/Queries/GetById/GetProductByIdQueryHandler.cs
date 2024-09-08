using AutoMapper;
using Inventory_Management_System.Application.Dtos.InventoryDto.ResponseDto;
using Inventory_Management_System.Application.IUnitOfWork;
using MediatR;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Queries.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductsResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public GetProductByIdQueryHandler(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<GetProductsResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
           var productData = await _unitOfWorkRepository.ProductDetailRepository.GetByIdAsync(request.Id);
            return _mapper.Map<GetProductsResponseDto>(productData);
        }
    }
}
