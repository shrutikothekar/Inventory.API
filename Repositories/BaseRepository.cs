using Inventory.API.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Inventory.API.Repositories
{
    public abstract class BaseRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        protected BaseRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        protected SqlConnection GetConnection()
        {
            return _dbFactory.CreateConnection();
        }

        protected SqlCommand CreateCommand(
            string commandText,
            SqlConnection connection,
            CommandType commandType = CommandType.StoredProcedure)
        {
            return new SqlCommand(commandText, connection)
            {
                CommandType = commandType
            };
        }

        public SqlConnection CreateConnection()
        {
            throw new NotImplementedException();
        }
    }
}
