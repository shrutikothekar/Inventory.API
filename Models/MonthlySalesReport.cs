namespace Inventory.API.Models
{
    public class MonthlySalesReport
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Revenue { get; set; }
        public int TotalOrders { get; set; }
    }
}
