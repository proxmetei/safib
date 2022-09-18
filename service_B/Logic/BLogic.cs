using System;
using System.Collections.Generic;
using System.Threading;
using service_A.Models.SubDivision;
namespace service_A.Logic
{
    public class ALogic
    {
        SubDivisionRepository repos;
        public ALogic(SubDivisionRepository _repos)
        {
            repos = _repos;
        }
        private void ChangeSubdivision(SubDivision subDivision) {
            subDivision.changeState();
        }
        private void ChangeSubdivisionDb(SubDivision subDivision)
        {
            repos.updateSubdivision(subDivision);
        }
        public  void ChangeSubdivisions(Object obj) {
            List<SubDivision> subDivisions = repos.getSubdivisions();
            subDivisions.ForEach(ChangeSubdivision);
            subDivisions.ForEach(ChangeSubdivisionDb);
        }

        public void createTimer() {
            TimerCallback tm = new TimerCallback(ChangeSubdivisions);
            Timer timer = new Timer(tm, 0, 0, 2000);
        }
    }
}
