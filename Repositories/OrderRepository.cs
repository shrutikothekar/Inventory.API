using Inventory.API.Data;
using Inventory.API.Models;
using Inventory.API.Services;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Inventory.API.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(IDbConnectionFactory dbFactory)
                    : base(dbFactory)
        {
        }
        
        public async Task<long> CreateOrderAsync(CreateOrderRequest request)
        {
            using var con = GetConnection();
            using var cmd = CreateCommand("dbo.usp_CreateOrder", con);

            cmd.CommandType = CommandType.StoredProcedure;

            // Order No
            cmd.Parameters.AddWithValue("@OrderNo", request.OrderNo);

            // Create DataTable for TVP
            var table = new DataTable();
            table.Columns.Add("ProductId", typeof(long));
            table.Columns.Add("Qty", typeof(int));
            table.Columns.Add("Price", typeof(decimal));

            foreach (var item in request.Items)
            {
                table.Rows.Add(item.ProductId, item.Qty, item.Price);
            }

            // TVP parameter
            var itemsParam = cmd.Parameters.AddWithValue("@Items", table);
            itemsParam.SqlDbType = SqlDbType.Structured;
            itemsParam.TypeName = "dbo.OrderItemType";

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return 1; // success
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int pageNo, int pageSize)
        {
            List<Order> orders = new();

            using var con = GetConnection();
            using var cmd = new SqlCommand("dbo.usp_GetOrdersPaged", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PageNo", SqlDbType.Int).Value = pageNo;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

            await con.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                orders.Add(new Order
                {
                    OrderId = reader.GetInt64(reader.GetOrdinal("OrderId")),
                    OrderNo = reader.GetString(reader.GetOrdinal("OrderNo")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                });
            }

            return orders;
        }

    }

}
