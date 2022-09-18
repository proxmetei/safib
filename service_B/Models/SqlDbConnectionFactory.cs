using System.Data;

namespace service_B.Models
{
    public class SqlDbConnectionFactory : IDbConnectionFactory
    {
        private string _connectionString;

        public SqlDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetDbConnection()
        {
            return new Npgsql.NpgsqlConnection(_connectionString);
        }
    }
}