using Inventory_Management_System.Application.IGeneric;
using Inventory_Management_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Infrastructure.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly InventoryDbContext _inventoryDbContext;
        public GenericRepository(InventoryDbContext inventoryDb)
        {
            _inventoryDbContext = inventoryDb;
        }
        public IQueryable<T> GetAllAsync(params string[] includeProperties)
        {
            var products = _inventoryDbContext.Set<T>().AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                products = products.Include(includeProperty);
            }
            return products;
        }
       
        public async Task CreateAsync(T entity) => await _inventoryDbContext.Set<T>().AddAsync(entity);

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _inventoryDbContext.Set<T>().FindAsync(id);

        }

        public Task UpdateAsync(T entity)
        {
            _inventoryDbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T existngData)
        {
            _inventoryDbContext.Set<T>().Update(existngData);
            return Task.CompletedTask;
        }

    }
}
