using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KrisApp.Models.Admin;
using KrisApp.DataAccess;

namespace KrisApp.Services
{
    public class LogService : AbstractService
    {
        public LogService(KrisLogger log) : base(log)
        {}

        /// <summary>
        /// Zwraca wszystkie logi
        /// </summary>
        internal LogsViewModel GetLogsAll()
        {
            LogsViewModel model = new LogsViewModel();

            using (KrisDbContext context = new KrisDbContext())
            {
                model.AppLogs = context.AppLogs.OrderByDescending(x => x.ID).ToList();
            }

            return model;
        }
    }
}