using Inventory_Management_System.Application.Services.InventoryService;
using Inventory_Management_System.Domain.Entities.InventoryDbEntities;
using Inventory_Management_System.Infrastructure.Data;
using Inventory_Management_System.Infrastructure.Generic;

namespace Inventory_Management_System.Infrastructure.Repositories
{
    public class InventoryRequestRepository : GenericRepository<InventoryRequestDetails>, IInventoryRequestRepository
    {
        private readonly InventoryDbContext _inventoryDbContext;
        public InventoryRequestRepository(InventoryDbContext inventoryDb) : base(inventoryDb)
        {
            _inventoryDbContext = inventoryDb;
        }
    }
}
