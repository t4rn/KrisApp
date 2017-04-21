using KrisApp.DataAccess;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Work;
using KrisApp.Models.Work;
using System.Collections.Generic;
using System;
using KrisApp.DataModel.Dictionaries;

namespace KrisApp.Services
{
    public class WorkerService : AbstractService
    {
        private readonly IWorkerRepository _workerRepo;
        private readonly DictionaryService _dictSrv;

        public WorkerService(KrisLogger log) : base(log)
        {
            _workerRepo = new WorkerRepo(Properties.Settings.Default.csDB);
            _dictSrv = new DictionaryService(log);
        }

        internal List<WorkerModel> GetWorkers()
        {
            List<WorkerModel> workersModel = new List<WorkerModel>();

            // pobieramy dane z bazy
            List<Worker> workers = _workerRepo.GetWorkers();

            // mapujemy encje z bazy na modele
            workersModel = MapService.MapWorkersToModel(workers);

            return workersModel;
        }

        /// <summary>
        /// Zwraca pracownika o danym ID
        /// </summary>
        internal WorkerModel GetWorkerByID(int id)
        {
            WorkerModel workerModel = null;

            Worker worker = _workerRepo.GetWorker(id);  

            if (worker != null)
            {
                workerModel = MapService.MapWorkerToModel(worker);
            }

            return workerModel;
        }

        internal List<PositionType> GetPositionTypes()
        {
            List<PositionType> positionTypes = _dictSrv.GetDictionary<PositionType>();

            return positionTypes;
        }

        internal List<SkillType> GetSkillTypes()
        {
            List<SkillType> skillTypes = _dictSrv.GetDictionary<SkillType>();

            return skillTypes;
       
        }
    }
}