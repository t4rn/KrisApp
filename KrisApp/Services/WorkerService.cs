using KrisApp.DataAccess;
using KrisApp.DataModel.Work;
using KrisApp.Models.Work;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace KrisApp.Services
{
    public class WorkerService : AbstractService
    {
        private KrisDbContext db = new KrisDbContext();

        public WorkerService(KrisLogger log) : base(log)
        { }

        internal List<WorkerModel> GetWorkers()
        {
            List<WorkerModel> workersModel = new List<WorkerModel>();

            db.Database.Log = log => System.Diagnostics.Debug.WriteLine(log);

            // pobieramy dane z bazy
            List<Worker> workers = db.Workers
                .Include(x => x.Positions.Select(p => p.Position))
                .Include(x => x.Skills.Select(s => s.Skill))
                .ToList();

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

            db.Database.Log = log => System.Diagnostics.Debug.WriteLine(log);

            Worker worker = db.Workers
                .Include(x => x.Positions.Select(p => p.Position))
                .Include(x => x.Skills.Select(s =>s.Skill))
                .FirstOrDefault(x => x.ID == id);

            if (worker != null)
            {
                workerModel = MapService.MapWorkerToModel(worker);
            }

            return workerModel;
        }
    }
}