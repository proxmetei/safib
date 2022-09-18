using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace service_A.Models
{
        public interface IDbConnectionFactory
        {
            IDbConnection GetDbConnection();
        }
}
