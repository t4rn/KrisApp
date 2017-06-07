using System;

namespace KrisApp.Common.Extensions
{
    public static class ExceptionExtensions
    {
        public static string MessageFromInnerEx(this Exception ex)
        {
            if (ex.InnerException != null)
            {
                return ex.InnerException.MessageFromInnerEx();
            }
            else
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Returns the most inner exception
        /// </summary>
        public static Exception ExceptionFromInnerEx(this Exception ex)
        {
            if (ex.InnerException != null)
            {
                return ex.InnerException.ExceptionFromInnerEx();
            }
            else
            {
                return ex;
            }
        }
    }
}
