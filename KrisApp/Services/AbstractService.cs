using System;
using System.Diagnostics;

namespace KrisApp.Services
{
    public abstract class AbstractService
    {
        protected readonly KrisLogger _log;

        public AbstractService(KrisLogger log)
        {
            _log = log;
        }

        protected Action<string> LogDb()
        {
            return msg => Debug.WriteLine(msg);
        }
    }
}