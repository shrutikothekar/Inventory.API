using Microsoft.Data.SqlClient;

namespace Inventory.API.Data
{
    public interface IDbConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
