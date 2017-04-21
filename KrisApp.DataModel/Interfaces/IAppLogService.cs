using KrisApp.DataModel.Logs;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces
{
    public interface IAppLogService
    {
        List<AppLog> GetLogsAll();
    }
}
