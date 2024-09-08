using Inventory_Management_System.Shared.Response;
using MediatR;
using static Inventory_Management_System.Shared.Enums.CategoryEnum;

namespace Inventory_Management_System.Application.Features.ProductDetailsFeature.Commands.Create
{
    public class AddProductCommand:IRequest<ResponseModal>
    {
        public string ProductName { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
