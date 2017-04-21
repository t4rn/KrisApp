using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Logs;
using System.Collections.Generic;

namespace KrisApp.Services
{
    public class LogService : AbstractService, IAppLogService
    {
        private readonly ILogRepository _logRepo;

        public LogService(ILogger log, ILogRepository logRepo) : base(log)
        {
            _logRepo = logRepo;
        }

        /// <summary>
        /// Zwraca wszystkie logi
        /// </summary>
        public List<AppLog> GetLogsAll()
        {
            return _logRepo.GetLogs();
        }
    }
}