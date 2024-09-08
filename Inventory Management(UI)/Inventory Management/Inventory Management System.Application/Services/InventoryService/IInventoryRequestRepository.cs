using Inventory_Management_System.Application.IGeneric;
using Inventory_Management_System.Domain.Entities.InventoryDbEntities;

namespace Inventory_Management_System.Application.Services.InventoryService
{
    public interface IInventoryRequestRepository:IGenericRepository<InventoryRequestDetails>
    {
    }
}
