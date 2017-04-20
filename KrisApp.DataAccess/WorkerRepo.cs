using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Work;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace KrisApp.DataAccess
{
    public class WorkerRepo : BaseDAL, IWorkerRepository
    {
        public WorkerRepo(string cs) : base(cs)
        {
        }

        public Worker GetWorker(int id)
        {
            Worker worker = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                worker = context.Workers.AsNoTracking()
                    .Include(x => x.Positions.Select(p => p.Position))
                    .Include(x => x.Skills.Select(s => s.Skill))
                    .FirstOrDefault(x => x.ID == id);
            }

            return worker;
        }

        public List<Worker> GetWorkers()
        {
            List<Worker> workers = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                workers = context.Workers.AsNoTracking()
                    .Include(x => x.Positions.Select(p => p.Position))
                    .Include(x => x.Skills.Select(s => s.Skill))
                    .ToList();
            }

            return workers;
        }
    }
}
