using AutoMapper;
using Inventory_Management_System.Application.IUnitOfWork;
using Inventory_Management_System.Domain.Entities.InventoryDbEntities;
using Inventory_Management_System.Shared.Response;
using MediatR;
using static Inventory_Management_System.Shared.Constants.CommonConstantsValues;

namespace Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Commands.Create
{
    public class AddInventoryRequestCommandHandler : IRequestHandler<AddInventoryRequestCommand, ResponseModal>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public AddInventoryRequestCommandHandler(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<ResponseModal> Handle(AddInventoryRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModal();
            var productDetailsModal = _mapper.Map<InventoryRequestDetails>(request);
           
            var existingProduct = await _unitOfWorkRepository.ProductDetailRepository.GetByIdAsync(productDetailsModal.ProductId);

            if(existingProduct ==  null)
            {
                response.IsSucceeded = false;
                response.DescriptionMessage = DataNotFound;
                return response;
            }

            var remainingQuantity = existingProduct.Quantity - request.Quantity;
            existingProduct.Quantity = remainingQuantity;

            await _unitOfWorkRepository.InventoryRequestRepository.CreateAsync(productDetailsModal);
            await _unitOfWorkRepository.ProductDetailRepository.UpdateAsync(existingProduct);
            await _unitOfWorkRepository.SaveAsync();

            response.IsSucceeded = true;
            response.Data = request;
            response.DescriptionMessage = InventoryRequestAdded;
            return response;
        }
    }
}
