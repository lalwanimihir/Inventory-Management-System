using Inventory_Management_System.Shared.Response;
using MediatR;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Update
{
    public class UpdateProductAvailabilityCommand:IRequest<ResponseModal>
    {
        public int Id { get; set; }
    }
}
