using System.Collections.Generic;
using KrisApp.DataModel.Work;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IWorkerRepository
    {
        List<Worker> GetWorkers();
        Worker GetWorker(int id);
    }
}
