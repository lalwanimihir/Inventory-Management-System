using Inventory_Management_System.Application.Services.InventoryService;

namespace Inventory_Management_System.Application.IUnitOfWork
{
    public interface IUnitOfWorkRepository :IDisposable
    {
        IProductDetailRepository ProductDetailRepository { get; }
        IInventoryRequestRepository InventoryRequestRepository { get; }

        Task SaveAsync();
    }
}
