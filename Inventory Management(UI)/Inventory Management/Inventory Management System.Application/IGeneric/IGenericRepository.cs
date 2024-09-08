namespace Inventory_Management_System.Application.IGeneric
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAllAsync(params string[] includeProperties);
        Task CreateAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T existngData);
    }
}
