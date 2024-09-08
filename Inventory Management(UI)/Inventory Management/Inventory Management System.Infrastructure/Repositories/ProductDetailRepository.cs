using Inventory_Management_System.Application.Services.InventoryService;
using Inventory_Management_System.Domain.Entities.InventoryDbEntities;
using Inventory_Management_System.Infrastructure.Data;
using Inventory_Management_System.Infrastructure.Generic;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Infrastructure.Repositories
{
    public class ProductDetailRepository:GenericRepository<ProductDetails>, IProductDetailRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;
        public ProductDetailRepository(InventoryDbContext inventoryDb) : base(inventoryDb)
        {
            _inventoryDbContext = inventoryDb;
        }

        public async Task<bool>  CheckAvailableQuantity(int productId,int quantity)
        {
            var isAvailable = await _inventoryDbContext.ProductDetails
               .AnyAsync(x => x.Id == productId && x.Quantity >= quantity);

            return isAvailable;
        }
       

    }
}
