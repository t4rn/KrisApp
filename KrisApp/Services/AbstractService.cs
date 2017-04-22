using KrisApp.Common.Extensions;
using KrisApp.DataModel.Interfaces;
using System;
using System.Diagnostics;

namespace KrisApp.Services
{
    public abstract class AbstractService
    {
        protected readonly ILogger _log;

        public AbstractService(ILogger log)
        {
            _log = log;
        }

        protected Action<string> LogDb()
        {
            return msg => Debug.WriteLine(msg);
        }

        protected void ExceptionLog(string methodName, Exception ex)
        {
            _log.Error("[{0}] Ex: Msg = {1} StackTrace = {2}",
                ex.MessageFromInnerEx(), ex.StackTrace);
        }
    }
}