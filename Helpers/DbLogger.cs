using Microsoft.Data.SqlClient;

namespace Inventory.API.Helpers
{
    public class DbLogger
    {
        private readonly string _connectionString;

        // Step 2.1 – inject IConfiguration
        public DbLogger(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public void LogToDb(string message, string level = "Information")
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Logs (Timestamp, Message, Level) VALUES (@time, @msg, @level)", conn);
                cmd.Parameters.AddWithValue("@time", DateTime.Now);
                cmd.Parameters.AddWithValue("@msg", message);
                cmd.Parameters.AddWithValue("@level", level);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
