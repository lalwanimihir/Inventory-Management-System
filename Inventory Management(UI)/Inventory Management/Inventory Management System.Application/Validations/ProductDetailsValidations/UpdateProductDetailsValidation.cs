using FluentValidation;
using Inventory_Management_System.Application.Dtos.InventoryDto.RequestDto;

namespace Inventory_Management_System.Application.Validations.ProductDetailsValidations
{
    public class UpdateProductDetailsValidation:AbstractValidator<UpdateProductRequestDto>
    {
        public UpdateProductDetailsValidation() {
            RuleFor(x => x.Id).NotNull().WithMessage("Id can't be null!");
            RuleFor(x => x.ProductName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Product name is required!");
            RuleFor(x => x.Category)
                .NotEmpty()
                .NotNull()
                .WithMessage("Category is required!")
                .IsInEnum()
                .WithMessage("Category must be valid!");
            RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage("Quantity is required!")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity must be positive value!");
            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("Price is required!")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be positive value!");
        }
    }
}
