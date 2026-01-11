namespace Inventory.API.Models
{
    public class CreateOrderRequest
    {
        public string OrderNo { get; set; }
        public List<OrderItem> Items { get; set; }
    }

}
