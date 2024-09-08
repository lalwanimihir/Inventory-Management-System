using FluentValidation;
using Inventory_Management_System.Application.Dtos.InventoryDto.RequestDto;

namespace Inventory_Management_System.Application.Validations.QueryValidation
{
    public class QueryFilterValidation:AbstractValidator<QueryRequestDto>
    {
        public QueryFilterValidation()
        {
            RuleFor(x => x.PageNo).GreaterThanOrEqualTo(1).LessThanOrEqualTo(1000).WithMessage("Page size should be greater than 1 and less than 1000");
        }
    }
}
