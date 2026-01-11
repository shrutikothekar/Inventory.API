using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Inventory.API.Data
{
	public class DbConnectionFactory : IDbConnectionFactory
	{
		private readonly IConfiguration _configuration;

		public DbConnectionFactory(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public SqlConnection CreateConnection()
		{
			return new SqlConnection(
				_configuration.GetConnectionString("DefaultConnection"));
		}
	}
}