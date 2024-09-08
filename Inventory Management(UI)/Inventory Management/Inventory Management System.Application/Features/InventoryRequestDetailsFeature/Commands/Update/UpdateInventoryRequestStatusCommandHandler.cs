using Inventory_Management_System.Application.IUnitOfWork;
using Inventory_Management_System.Shared.Response;
using MediatR;
using static Inventory_Management_System.Shared.Constants.CommonConstantsValues;

namespace Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Commands.Update
{
    public class UpdateInventoryRequestStatusCommandHandler : IRequestHandler<UpdateInventoryRequestStatusCommand, ResponseModal>
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public UpdateInventoryRequestStatusCommandHandler( IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<ResponseModal> Handle(UpdateInventoryRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModal();
            var existingInventory = await _unitOfWorkRepository.InventoryRequestRepository.GetByIdAsync(request.Id);
            if (existingInventory == null)
            {
                response.IsSucceeded = false;
                response.DescriptionMessage = DataNotFound;
                return response;
            }
            existingInventory.Status = request.Status;

            await _unitOfWorkRepository.InventoryRequestRepository.UpdateAsync(existingInventory);
            await _unitOfWorkRepository.SaveAsync();

            response.IsSucceeded = true;
            response.Data = request;
            response.DescriptionMessage = StatusChanged;

            return response;
        }
    }
}
