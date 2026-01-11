using Inventory.API.Models;
using Inventory.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            await _orderRepository.CreateOrderAsync(request);
            return Ok("Order created successfully");
        }
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders(
           int pageNo = 1,
           int pageSize = 20)
        {
            var data = await _orderRepository.GetOrdersAsync(pageNo, pageSize);
            return Ok(data);
        }
    }
}
