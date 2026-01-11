using Inventory.API.Models;

namespace Inventory.API.Services
{
    public interface IOrderRepository
    {
        Task<long> CreateOrderAsync(CreateOrderRequest request);
        Task<IEnumerable<Order>> GetOrdersAsync(int pageNo, int pageSize);

    }
}
