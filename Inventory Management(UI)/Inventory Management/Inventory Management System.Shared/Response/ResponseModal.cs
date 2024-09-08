namespace Inventory_Management_System.Shared.Response
{
    public class ResponseModal
    {
        public bool IsSucceeded { get; set; }
        public object? Data { get; set; }
        public string? DescriptionMessage { get; set; }
    }
}
