using AutoMapper;
using Inventory_Management_System.Application.IUnitOfWork;
using Inventory_Management_System.Domain.Entities.InventoryDbEntities;
using Inventory_Management_System.Shared.Response;
using MediatR;
using static Inventory_Management_System.Shared.Constants.CommonConstantsValues;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Create
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ResponseModal>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public AddProductCommandHandler(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<ResponseModal> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModal();

            var productDetailsModal = _mapper.Map<ProductDetails>(request);

            await _unitOfWorkRepository.ProductDetailRepository.CreateAsync(productDetailsModal);
            await _unitOfWorkRepository.SaveAsync();

            response.IsSucceeded = true;
            response.Data = request;
            response.DescriptionMessage = ProductAdded;
            return response;
        }
    }
}
