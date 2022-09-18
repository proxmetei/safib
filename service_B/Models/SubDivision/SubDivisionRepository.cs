using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace service_B.Models.SubDivision
{
    /// <summary>
    ///  Класс SubDivisionRepository
    ///  класс отвечает за работу с БД, хранящей подраделения
    /// </summary>
    public class SubDivisionRepository
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly IDbConnection connection;
        static HttpClient client = new HttpClient();

        public SubDivisionRepository(IDbConnectionFactory _connectionFactory)
        {
            connectionFactory = _connectionFactory;
            connection = connectionFactory.GetDbConnection();
        }
        /// <summary>
        /// Метод getSubdivisions() отвечает за
        /// получение данных из БД
        /// </summary>
        public List<SubDivision> getSubdivisions()
        {
            List<SubDivision> subDivisions = connection.Query<SubDivision>("SELECT A.id, A.name, A.included_in_id  FROM company.subdivision as A").ToList();
            return subDivisions;

        }
        /// <summary>
        /// Метод getSubdivisionsStatuses() отвечает за
        /// получение данных из APi
        /// </summary>
        public async Task<List<SubDivision>> getSubdivisionsStatuses()
        {
                List<SubDivision> subDivisions = new List<SubDivision>();
                client.DefaultRequestHeaders.Add("apiKey", "MySecretKey");
                HttpResponseMessage response = await client.GetAsync("https://localhost:5005/status/get");
                if (response.IsSuccessStatusCode)
                {
                    //string str = await response.Content.ReadAsStringAsync();
                    subDivisions = await response.Content.ReadAsAsync<List<SubDivision>>();
                }
                return subDivisions;
            

        }
        /// <summary>
        /// Метод updateSubdivisions() отвечает за
        /// измение данных подразделения в БД
        /// </summary>
        /// <param name="subDivision">Аргумент метода updateSubdivision(), данные подразделения</param>
        public void updateSubdivision(SubDivision subDivision)
        {
            string sqlUpd = "UPDATE company.subdivision SET name=@NA, included_in_id = @NID " +
                    "WHERE id = @ID";
            connection.Execute(sqlUpd, new { ID = subDivision.Id, 
                NA = subDivision.Name,                 NID = subDivision.Included_in_Id==0?null: subDivision.Included_in_Id});
        }
        /// <summary>
        /// Метод addSubdivision() отвечает за
        /// добавление данных подразделение в БД
        /// </summary>
        /// <param name="subDivision">Аргумент метода addSubdivision(), данные подразделения</param>
        public void addSubdivision(SubDivision subDivision)
        {
            string sqlInsert = "INSERT INTO company.subdivision( name, included_in_id) " +
    "VALUES (@NA, @NID)";
            connection.Execute(sqlInsert, new
            {
                NA = subDivision.Name,
                NID = subDivision.Included_in_Id==0?null: subDivision.Included_in_Id
            });;;
        }
        /// <summary>
        /// Метод notifServiceA() отвечает за
        /// уведомление сервиса A об изменении данных в БД
        /// </summary>
        public async Task notifServiceA() {
           await client.GetAsync("https://localhost:5005/status/start");
           
        }
    }
}
