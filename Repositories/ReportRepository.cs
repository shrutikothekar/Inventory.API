using Inventory.API.Data;
using Inventory.API.Models;
using Inventory.API.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Inventory.API.Repositories
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public ReportRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        public async Task<IEnumerable<MonthlySalesReport>> GetMonthlySalesAsync()
        {
            List<MonthlySalesReport> reports = new();

            using var con = GetConnection();
            var Query = "SELECT * FROM dbo.vw_MonthlySales";
            using var cmd = CreateCommand(Query, con, CommandType.Text);

            await con.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                reports.Add(new MonthlySalesReport
                {
                    Month = reader.GetInt32(0),
                    Year = reader.GetInt32(1),
                    Revenue = reader.GetDecimal(2),
                    TotalOrders = reader.GetInt32(3)
                });
            }

            return reports;
        }
    }
}


