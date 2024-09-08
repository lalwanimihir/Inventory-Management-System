namespace Inventory_Management_System.Shared.Response
{
    public class ActiveProductsResponse
    {
        public bool IsSucceeded { get; set; }
        public object? ActiveProducts { get; set; }
        public string? DescriptionMessage { get; set; }
    }
}
