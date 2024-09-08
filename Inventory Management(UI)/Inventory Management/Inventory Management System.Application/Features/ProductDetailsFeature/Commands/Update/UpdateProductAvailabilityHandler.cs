using AutoMapper;
using Inventory_Management_System.Application.IUnitOfWork;
using Inventory_Management_System.Shared.Response;
using MediatR;
using static Inventory_Management_System.Shared.Constants.CommonConstantsValues;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Update
{
    public class UpdateProductAvailabilityHandler:IRequestHandler<UpdateProductAvailabilityCommand, ResponseModal>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public UpdateProductAvailabilityHandler(IMapper mapper, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _mapper = mapper;
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        public async Task<ResponseModal> Handle(UpdateProductAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModal();
            var existingProduct = await _unitOfWorkRepository.ProductDetailRepository.GetByIdAsync(request.Id);
            if (existingProduct == null)
            {
                response.IsSucceeded = false;
                response.DescriptionMessage = DataNotFound;
                return response;
            }
            existingProduct.IsActive = !existingProduct.IsActive;

            await _unitOfWorkRepository.ProductDetailRepository.UpdateAsync(existingProduct);
            await _unitOfWorkRepository.SaveAsync();

            response.IsSucceeded = true;
            response.Data = request;
            response.DescriptionMessage = DataUpdated;
            return response;
        }
    }
}
