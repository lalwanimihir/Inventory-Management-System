using Inventory_Management_System.Shared.Response;
using MediatR;

namespace Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Commands.Create
{
    public class AddInventoryRequestCommand:IRequest<ResponseModal>
    {
         public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public string Reason { get; set; }
        public string UserId { get; set; }
    }
}
