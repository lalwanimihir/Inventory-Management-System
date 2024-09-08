using Inventory_Management_System.Application.IUnitOfWork;
using Inventory_Management_System.Application.Services.InventoryService;
using Inventory_Management_System.Infrastructure.Data;
using Inventory_Management_System.Infrastructure.Repositories;

namespace Inventory_Management_System.Infrastructure.UnitOfWork
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public UnitOfWorkRepository(InventoryDbContext inventoryDb)
        {
            _inventoryDbContext = inventoryDb;
            ProductDetailRepository = new ProductDetailRepository(inventoryDb);
            InventoryRequestRepository = new InventoryRequestRepository(inventoryDb);
        }
        public IProductDetailRepository ProductDetailRepository { get; private set; }
        public IInventoryRequestRepository InventoryRequestRepository { get; private set; }


        public void Dispose()
        {
            _inventoryDbContext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _inventoryDbContext.SaveChangesAsync();
        }
    }
}
