using Inventory.API.Models;

namespace Inventory.API.Services
{
    public interface IReportRepository
    {
        Task<IEnumerable<MonthlySalesReport>> GetMonthlySalesAsync();
    }
}
