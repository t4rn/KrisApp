using KrisApp.DataAccess;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.Models.Admin;
using System.Linq;

namespace KrisApp.Services
{
    public class LogService : AbstractService
    {
        private readonly ILogRepository _logRepo;

        public LogService(KrisLogger log) : base(log)
        {
            _logRepo = new AppLogRepo(Properties.Settings.Default.csDB);
        }

        /// <summary>
        /// Zwraca wszystkie logi
        /// </summary>
        internal LogsViewModel GetLogsAll()
        {
            LogsViewModel model = new LogsViewModel();

            model.AppLogs = _logRepo.GetLogs();


            return model;
        }
    }
}