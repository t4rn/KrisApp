using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Work;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces
{
    public interface IWorkerService
    {
        List<Worker> GetWorkers();

        /// <summary>
        /// Zwraca pracownika o danym ID
        /// </summary>
        Worker GetWorkerByID(int id);

        List<PositionType> GetPositionTypes();

        List<SkillType> GetSkillTypes();
    }
}
