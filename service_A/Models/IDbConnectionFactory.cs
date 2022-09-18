using System.Data;
namespace service_A.Models
{
    public interface IDbConnectionFactory
        {
            IDbConnection GetDbConnection();
        }
}
