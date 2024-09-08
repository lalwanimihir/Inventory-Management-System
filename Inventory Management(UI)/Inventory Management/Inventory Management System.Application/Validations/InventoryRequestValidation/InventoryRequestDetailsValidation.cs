using FluentValidation;
using Inventory_Management_System.Application.Dtos.InventoryDto.RequestDto;
using Inventory_Management_System.Application.IUnitOfWork;

namespace Inventory_Management_System.Application.Validations.InventoryRequestValidation
{
    public class InventoryRequestDetailsValidation : AbstractValidator<AddInventoryRequestDto>
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public InventoryRequestDetailsValidation(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            RuleFor(x => x.ProductId).NotNull().WithMessage("Product id can't be null");
            RuleFor(x => x.ProductName)
               .NotEmpty()
               .NotNull()
               .WithMessage("Product name is required!");
            RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage("Reason is required!")
                .GreaterThan(0)
                .WithMessage("Quantity should be greater than zero!");
            RuleFor(x => new { x.ProductId, x.Quantity })
                .NotEmpty()
                .NotNull()
                .WithMessage("Quantity is required!")
                .Must(x => IsQuantityAvailable(x.ProductId, x.Quantity))
                .WithMessage("Quantity is more than available stock");
            RuleFor(x => x.Reason)
                .NotEmpty()
                .NotNull()
                .WithMessage("Reason is required!");
        }

        private bool IsQuantityAvailable(int productId, int quantity)
        {
            var isQuantity = _unitOfWorkRepository.ProductDetailRepository.CheckAvailableQuantity(productId, quantity);
            if (isQuantity.Result == false)
            {
                return false;
            }
            return true;
        }

    }
}
