using System.Data;
namespace service_B.Models
{
        public interface IDbConnectionFactory
        {
            IDbConnection GetDbConnection();
        }
}
