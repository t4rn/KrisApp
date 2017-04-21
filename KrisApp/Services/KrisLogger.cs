using KrisApp.Common.Extensions;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Logs;

namespace KrisApp.Services
{
    public class KrisLogger : ILogger
    {
        private readonly ILogRepository _logDAL;

        public KrisLogger(ILogRepository logRepo)
        {
            _logDAL = logRepo;
        }

        public void Error(string format, params object[] args)
        {
            AppLog log = PrepareLog(AppLog.LogType.ERROR, format, args);

            AddLogToDB(log);
        }

        /// <summary>
        /// Zapisuje przekazaną wiadomość na bazie [WWW.Logs]
        /// </summary>
        public void Debug(string format, params object[] args)
        {
            AppLog log = PrepareLog(AppLog.LogType.DEBUG, format, args);

            AddLogToDB(log);
        }

        /// <summary>
        /// Zapisuje log na bazie, jeżeli nie podchodzi z localhost
        /// </summary>
        private void AddLogToDB(AppLog log)
        {
            if (!log.Ip.In("::1", "127.0.0.1"))
            {
                _logDAL.AddLog(log);
            }
        }

        /// <summary>
        /// Zwraca AppLog wypełniony danymi
        /// </summary>
        private AppLog PrepareLog(AppLog.LogType logType, string format, object[] args)
        {
            return new AppLog()
            {
                Type = logType.ToString(),
                Message = string.Format(format, args),
                Ip = NetworkService.GetContextIP(),
            };
        }
    }
}