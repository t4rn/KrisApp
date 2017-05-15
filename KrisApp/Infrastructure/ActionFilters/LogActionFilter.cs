using KrisApp.DataModel.Interfaces;
using System.Web.Mvc;

namespace KrisApp.Infrastructure
{
    public class LogActionFilter : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// Logger wstrzyknięty Autofac
        /// </summary>
        public ILogger Log { get; set; }

        /// <summary>
        /// Opis ustawiany w kontrolerze
        /// </summary>
        public string Desc { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log.Debug("[OnActionExecuting] Desc = '{0}' | Controller = '{1}' | Method = '{2}' | User = '{3}'",
                Desc,
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName,
                filterContext.HttpContext.User?.Identity?.Name);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log.Debug("[OnActionExecuted] Desc = '{0}' | User = '{1}'",
                Desc, filterContext.HttpContext.User?.Identity?.Name);
        }
    }
}