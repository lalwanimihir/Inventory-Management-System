using Inventory_Management_System.Application.IUnitOfWork;
using Inventory_Management_System.Shared.Response;
using MediatR;
using static Inventory_Management_System.Shared.Constants.CommonConstantsValues;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseModal>
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public UpdateProductCommandHandler( IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<ResponseModal> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModal();
            var existingProduct = await _unitOfWorkRepository.ProductDetailRepository.GetByIdAsync(request.Id);
            if (existingProduct == null)
            {
                response.IsSucceeded = false;
                response.DescriptionMessage = DataNotFound;
                return response;
            }

            existingProduct.ProductName = request.ProductName;
            existingProduct.Quantity = request.Quantity;
            existingProduct.Price = request.Price;
            existingProduct.Category = request.Category;

            await _unitOfWorkRepository.ProductDetailRepository.UpdateAsync(existingProduct);
            await _unitOfWorkRepository.SaveAsync();

            response.IsSucceeded = true;
            response.Data = request;
            response.DescriptionMessage = DataUpdated;
            return response;
        }
    }
}
