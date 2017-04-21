using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Work;
using System.Collections.Generic;

namespace KrisApp.Services
{
    public class WorkerService : AbstractService, IWorkerService
    {
        private readonly IWorkerRepository _workerRepo;
        private readonly IDictionaryService _dictSrv;

        public WorkerService(ILogger log, IWorkerRepository workerRepo, IDictionaryService dictSrv) : base(log)
        {
            _workerRepo = workerRepo;
            _dictSrv = dictSrv;
        }

        public List<Worker> GetWorkers()
        {
            // pobieramy dane z bazy
            List<Worker> workers = _workerRepo.GetWorkers();

            return workers;
        }

        /// <summary>
        /// Zwraca pracownika o danym ID
        /// </summary>
        public Worker GetWorkerByID(int id)
        {
            Worker worker = _workerRepo.GetWorker(id);

            return worker;
        }

        public List<PositionType> GetPositionTypes()
        {
            List<PositionType> positionTypes = _dictSrv.GetDictionary<PositionType>();

            return positionTypes;
        }

        public List<SkillType> GetSkillTypes()
        {
            List<SkillType> skillTypes = _dictSrv.GetDictionary<SkillType>();

            return skillTypes;

        }
    }
}