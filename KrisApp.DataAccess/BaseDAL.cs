using System;
using System.Diagnostics;

namespace KrisApp.DataAccess
{
    public abstract class BaseDAL
    {
        protected string csKris;

        public BaseDAL(string cs)
        {
            csKris = cs;
        }

        protected Action<string> LogDb()
        {
            return msg => Debug.WriteLine(msg);
        }
    }
}