using System;
using KrisApp.Common.Extensions;
using KrisApp.DataAccess;
using KrisApp.DataModel.Logs;

namespace KrisApp.Services
{
    public class KrisLogger
    {
        private readonly AppLogRepo _logDAL;

        public KrisLogger()
        {
            _logDAL = new AppLogRepo(Properties.Settings.Default.csDB);
        }

        public void Error(string format, params object[] args)
        {
            AppLog log = PrepareLog(AppLog.LogType.ERROR, format, args);

            AddLogToDB(log);
        }

        /// <summary>
        /// Zapisuje przekazaną wiadomość na bazie [WWW.Logs]
        /// </summary>
        internal void Debug(string format, params object[] args)
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