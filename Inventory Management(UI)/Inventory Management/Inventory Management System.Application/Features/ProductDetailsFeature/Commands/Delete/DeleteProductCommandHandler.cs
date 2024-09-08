using Inventory_Management_System.Application.IUnitOfWork;
using Inventory_Management_System.Shared.Response;
using MediatR;
using static Inventory_Management_System.Shared.Constants.CommonConstantsValues;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ResponseModal>
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public DeleteProductCommandHandler(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<ResponseModal> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModal();
            var existingProduct = await _unitOfWorkRepository.ProductDetailRepository.GetByIdAsync(request.Id);
            if (existingProduct == null)
            {
                response.IsSucceeded = false;
                response.DescriptionMessage = DataNotFound;
                return response;
            }
           
            existingProduct.IsDeleted = true;
            existingProduct.IsActive = false;
            existingProduct.DeletedDate = DateTime.UtcNow;
            existingProduct.DeletedBy = request.UserId;

            await _unitOfWorkRepository.ProductDetailRepository.DeleteAsync(existingProduct);
            await _unitOfWorkRepository.SaveAsync();

            response.IsSucceeded = true;
            response.Data = existingProduct;
            response.DescriptionMessage = DataDeleted;
            return response;
        }
    }
}
