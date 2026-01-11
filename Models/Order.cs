namespace Inventory.API.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        public string OrderNo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
