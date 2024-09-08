using Inventory_Management_System.Application.IGeneric;
using Inventory_Management_System.Domain.Entities.InventoryDbEntities;

namespace Inventory_Management_System.Application.Services.InventoryService
{
    public interface IProductDetailRepository : IGenericRepository<ProductDetails>
    {
        Task<bool> CheckAvailableQuantity(int productId,int quantity);
    }
}
