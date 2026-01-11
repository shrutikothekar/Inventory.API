using Inventory.API.Data;
using Inventory.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : Controller
    {
        private readonly IReportRepository _repo;
        private readonly IDbConnectionFactory _connectionFactory;
        public ReportsController(IReportRepository repo, IDbConnectionFactory connectionFactory)
        {
            _repo = repo;
            _connectionFactory = connectionFactory;
        }

        [HttpGet("monthly-sales")]
        public async Task<IActionResult> GetMonthlySales()
        {
            var data = await _repo.GetMonthlySalesAsync();
            return Ok(data);
        }
    }
}
