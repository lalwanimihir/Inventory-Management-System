using Inventory_Management_System.Shared.Response;
using MediatR;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Delete
{
    public class DeleteProductCommand:IRequest<ResponseModal>
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
    }
}
