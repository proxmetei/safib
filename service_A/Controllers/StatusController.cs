using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using service_A.Models.SubDivision;
using Microsoft.Extensions.Logging;
using service_A.Logic;
using System.Threading;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using service_A.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using service_A.Filters;

namespace service_A.Controllers
{
    /// <summary>
    ///  Класс HomeController
    ///  Является единственным котроллером серввиса А для
    ///  работы со статусами подразделений
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiKeyAuth]
    public class StatusController : ControllerBase
    {
        static HttpClient client = new HttpClient();
        Timer timer;
        SubDivisionRepository repos;
        ALogic logic;
        public StatusController(SubDivisionRepository _repos, ALogic _logic) {
            repos = _repos;
            logic = _logic;
            this.Start();
        }

        [HttpGet("Get")]
        /// <summary>
        /// Метод Get() отвечает за
        /// отправку данных статусов подразделений
        /// </summary>
        public IEnumerable<SubDivision> Get()
        {
            if (SubDivisionsST.subdivisions == null)
            {
                return new List<SubDivision>();
            }
            return
            SubDivisionsST.subdivisions.ToArray();
            
        }
        /// <summary>
        /// Метод Start() отвечает за
        ///  получение данных подразделений и создание таймера
        /// </summary>
        [HttpGet("Start")]
        public void Start()
        {
            if (timer != null)
            {
                // если таймер есть, то он удаляется
                timer.Dispose();
            }
            else {
                //await SendToken();
            }
            Update();
            
            timer = logic.createTimer();
        }
        /// <summary>
        /// Метод Update() отвечает за
        ///  получение данных подразделений
        /// </summary>
        public void Update()
        {
           SubDivisionsST.subdivisions = repos.getSubdivisions();
        }

    }
}
