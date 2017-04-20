using System.Collections.Generic;
using KrisApp.DataModel.Logs;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface ILogRepository
    {
        void AddLog(AppLog log);
        List<AppLog> GetLogs();
    }
}
