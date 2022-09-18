using System.Collections.Generic;
using service_B.Models.SubDivision;
using service_B.Models.Exceptions;
namespace service_B.Logic
{
    /// <summary>
    ///  Класс BLogic
    ///  класс содержит бизнес-логику сервиса B
    /// </summary>
    public class BLogic
    {
        SubDivisionRepository repos;
        public BLogic(SubDivisionRepository _repos)
        {
            repos = _repos;
        }
        /// <summary>
        /// Метод compareServises() отвечает за
        /// совмещение данных БД и API
        /// </summary>
        /// <param name="subDivisions1">Аргумент метода compareServises(), данные БД</param>
        /// <param name="subDivisions2">Аргумент метода compareServises(), данные API</param>
        public List<SubDivision> compareServises(List<SubDivision> subDivisions1, List<SubDivision> subDivisions2) {
            for (int i = 0; i < subDivisions1.Count; i++) {
                for (int j = 0; j < subDivisions2.Count; j++) {
                    if (subDivisions2[i].Id == subDivisions1[i].Id)
                    {
                        subDivisions1[i].Status = subDivisions2[i].Status;
                    }
                }
            }
            return subDivisions1;
        }
        /// <summary>
        /// Метод dfs() отвечает за
        /// проверку того, что не получится цикл в дереве 
        /// </summary>
        /// <param name="subDivisions">Аргумент метода dfs(), данные БД</param>
        /// <param name="id">Аргумент метода dfs(), id изменяемого подразделения</param>
        /// <param name="myId">Аргумент метода dfs(), included_id изменяемого подразделения</param>
        private bool dfs(List<SubDivision> subDivisions, int id, int myId) {
            for (int i = 0; i < subDivisions.Count; i++) {
                if (subDivisions[i].Included_in_Id == myId) {
                    if (subDivisions[i].Id == id)
                    {
                        return true;
                    }
                    else {
                        if (dfs(subDivisions, id, subDivisions[i].Id)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Метод checkContent() отвечает за
        /// проверку того, что все нужные данные присутсвуют
        /// </summary>
        /// <param name="subDivisions">Аргумент метода checkContent(), данные подразделений</param>
        private void checkContent(List<SubDivision> subDivisions) {
            subDivisions.ForEach((SubDivision subDivision)=> {
                if (subDivision.Name == null) {
                    throw new NotEnoughtDataException("Недостаточно имени");
                }
            });
        }
        /// <summary>
        /// Метод updateSubdivisions() отвечает за
        /// измение данных в БД на основе данных из файла
        /// </summary>
        /// <param name="subDivisions1">Аргумент метода checkContent(), данные подразделений из файла</param>
        /// <param name="subDivisions2">Аргумент метода checkContent(), данные подразделений из БД</param>
        public async System.Threading.Tasks.Task updateSubdivisions(List<SubDivision> subDivisions1, List<SubDivision> subDivisions2) {
            checkContent(subDivisions1);
            for (int i = 0; i < subDivisions1.Count; i++) {
                //SubDivision sub = subDivisions2.Find(x => x.Id == subDivisions1[i].Id);
                if (subDivisions1[i].Included_in_Id != null)
                    if (subDivisions2.Find(e => e.Id == subDivisions1[i].Included_in_Id.Value) == null) {
                        throw new NoSuchIncluddedException("Нет такого подзаделения в которое можно войти");
                    }
                for (int k = 0; k < subDivisions2.Count; k++)
                {
                    if (subDivisions1[i].Name == subDivisions2[k].Name && subDivisions1[i].Id!=subDivisions2[k].Id)
                    {

                        throw new UniqueNameException("Такое имя уже стущетсвует", subDivisions1[i].Name);
                    }
                }
                if (subDivisions1[i].Id == 0)
                {
                    repos.addSubdivision(subDivisions1[i]);
                }
                else {
                    if (subDivisions1[i].Included_in_Id != subDivisions2[i].Included_in_Id&&subDivisions1[i].Included_in_Id!=subDivisions1[i].Id) {
                        if (dfs(subDivisions2, subDivisions1[i].Included_in_Id==null?0: subDivisions1[i].Included_in_Id.Value, subDivisions1[i].Id)) {
                            throw new SubdivisionTreeException("Нельзя родительское подразделение сделать частью дочернего", subDivisions1[i].Name, subDivisions2.Find( sub =>
                             sub.Id == subDivisions1[i].Included_in_Id ).Name);
                        }
                    }
                    repos.updateSubdivision(subDivisions1[i]);
                }
            }
            await repos.notifServiceA();
        }
    }
}
