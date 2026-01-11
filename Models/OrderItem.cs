namespace Inventory.API.Models
{
    public class OrderItem
    {
        public long ProductId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }

}
