using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace service_A.Models.SubDivision
{
    /// <summary>
    ///  Класс SubDivisionRepository
    ///  класс отвечает за работу с БД, хранящей подраделения
    /// </summary>
    public class SubDivisionRepository
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly IDbConnection connection;

        public SubDivisionRepository(IDbConnectionFactory _connectionFactory)
        {
            connectionFactory = _connectionFactory;
            connection = connectionFactory.GetDbConnection();
        }
        /// <summary>
        /// Метод getSubdivisions() отвечает за
        /// получение данных из БД
        /// </summary>
        public List<SubDivision> getSubdivisions() {
            List<SubDivision> subDivisions = connection.Query<SubDivision>("SELECT * FROM company.subdivision").ToList();
            foreach (SubDivision subDivision in subDivisions) {
                subDivision.setState();
            }
            return subDivisions;

        }
    }
}
