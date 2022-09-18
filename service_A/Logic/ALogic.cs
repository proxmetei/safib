using System;
using System.Collections.Generic;
using System.Threading;
using service_A.Models.SubDivision;
namespace service_A.Logic
{
    /// <summary>
    ///  Класс ALogic
    ///  класс содержит бизнес-логику сервиса A
    /// </summary>
    public class ALogic
    {
        Timer timer;
        SubDivisionRepository repos;
        public ALogic(SubDivisionRepository _repos)
        {
            repos = _repos;
        }
        /// <summary>
        /// Метод ChangeSubdivision() отвечает за
        /// изменение статуса подразделения
        /// </summary>
        /// <param name="subDivision">Аргумент метода ChangeSubdivision(), данные подразделения</param>
        private void ChangeSubdivision(SubDivision subDivision) {
            subDivision.changeState();
        }
        /// <summary>
        /// Метод ChangeSubdivisions() отвечает за
        /// изменение статусов всех подразделений
        /// </summary>
        public void ChangeSubdivisions(Object _) {
            List<SubDivision> subDivisions = SubDivisionsST.subdivisions;
            subDivisions.ForEach(ChangeSubdivision);
            SubDivisionsST.subdivisions = subDivisions;
        }
        /// <summary>
        /// Метод ChangeSubdivisions() отвечает за
        /// создание таймера
        /// </summary>
        public Timer createTimer() {
            TimerCallback tm = new TimerCallback(ChangeSubdivisions);
             return timer = new Timer(tm, 0, 0, 3000);
        }
    }
}
