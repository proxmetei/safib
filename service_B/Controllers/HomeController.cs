using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using service_B.Models;
using service_B.Models.SubDivision;
using service_B.Logic;
using service_B.ViewModels;
using System.IO;
using Newtonsoft.Json;
using service_B.Models.Exceptions;


namespace service_B.Controllers
{
    /// <summary>
    ///  Класс HomeController
    ///  Является единственным котроллером серввиса B для
    ///  отображения данных подразделений и их изменеия
    /// </summary>
    public class HomeController : Controller
    {

        SubDivisionRepository repos;
        BLogic logic;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, SubDivisionRepository _repos, BLogic _logic)
        {
            repos = _repos;
            _logger = logger;
            logic = _logic;
        }
        /// <summary>
        /// Метод Index() отвечает за
        /// отображение основной страницы и передает данные для нее 
        /// </summary>
        public async Task<ActionResult> Index()
        {
            //получение данных из бд и api
            List<SubDivision> subDivisions = logic.compareServises(repos.getSubdivisions(), await repos.getSubdivisionsStatuses());
            //сессионное хранение
            TempData["sub"] = System.Text.Json.JsonSerializer.Serialize(subDivisions);
            //передача данных в представление
            ViewBag.subDivisions = subDivisions.ToArray();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// Метод Create() отвечает за
        /// синхронизацию данных с файлом
        /// </summary>
        /// <param name="model">Аргумент метода Create(), модель,содержащая файл</param>
        [HttpPost]
        public async Task<IActionResult> Create(FileModel model)
        {
            // Получение данных из файла
            byte[] post = null;
            if (model.File != null)
                using (var binaryReader = new BinaryReader(model.File.OpenReadStream()))
                {
                    post = binaryReader.ReadBytes((int)model.File.Length);
                }
            string str = System.Text.Encoding.UTF8.GetString(post);
            // Получение данных из сессионого хранилища и их оставление на случай повторной ошибки
            List<SubDivision> subDivisions1 = JsonConvert.DeserializeObject<List<SubDivision>>((string)TempData["sub"]);
            TempData.Keep();
            // Обновление бд в соответствии с файлом и обработка ошибок
            try
            {
                List<SubDivision> subDivisions = JsonConvert.DeserializeObject<List<SubDivision>>(str);
                await logic.updateSubdivisions(subDivisions, subDivisions1);
            }
            catch (Newtonsoft.Json.JsonException)
            {
                ModelState.AddModelError("", "Данные должны быть в формате JSON");
                ViewBag.subDivisions = subDivisions1;
                return View("Index");
            }
            catch (UniqueNameException ex)
            {
                ModelState.AddModelError("", ex.Message + ": "+ex.Name);
                ViewBag.subDivisions = subDivisions1;
                return View("Index");
            }
            catch (SubdivisionTreeException ex)
            {
                ModelState.AddModelError("", ex.Message + ", имя подразделения: " + ex.Name + ", имя входящего в него: " + ex.Included_name);
                ViewBag.subDivisions = subDivisions1;
                return View("Index");
            }
            catch (NotEnoughtDataException ex) {
                ModelState.AddModelError("", ex.Message);
                ViewBag.subDivisions = subDivisions1;
                return View("Index");
            }
            catch (NoSuchIncluddedException ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.subDivisions = subDivisions1;
                return View("Index");
            }
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public void setToken() {

        }
    }
}
