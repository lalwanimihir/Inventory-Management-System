using Inventory_Management_System.Shared.Response;
using MediatR;
using static Inventory_Management_System.Shared.Enums.StatusEnum;

namespace Inventory_Management_System.Application.Features.InventoryRequestDetailsFeature.Commands.Update
{
    public class UpdateInventoryRequestStatusCommand:IRequest<ResponseModal>
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}
