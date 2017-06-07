using KrisApp.Common.Extensions;
using KrisApp.DataModel.Interfaces;
using System.Web.Mvc;
using System;

namespace KrisApp.Infrastructure.ExceptionFilters
{
    public class ExcepionLoggingFilter : FilterAttribute, IExceptionFilter
    {
        private readonly ILogger _log;

        public ExcepionLoggingFilter(ILogger log)
        {
            _log = log;
        }

        public void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.ExceptionHandled)
            {
                return;
            }

            string controllerName = exceptionContext.Controller.GetType().Name;
            string actionName = exceptionContext.RouteData.Values["action"].ToString();
            string exceptionInnerMsg = exceptionContext.Exception.MessageFromInnerEx();

            _log.Error("Ex at '{0}/{1}' resultType '{2}' user '{3}' : msg = '{4}' st = {5}",
                controllerName,
                actionName,
                exceptionContext.Result.GetType().Name,
                exceptionContext.HttpContext?.User?.Identity?.Name,
                exceptionInnerMsg,
                exceptionContext.Exception.StackTrace);

            // request from WebAPI
            if (IsAjax(exceptionContext))
            {
                exceptionContext.Result = PrepareJsonResult(exceptionInnerMsg);
            }
            else
            {
                exceptionContext.Result = PrepareNormalResult(exceptionContext, controllerName, actionName);
            }

            exceptionContext.HttpContext.Response.StatusCode = 500;
            exceptionContext.ExceptionHandled = true;
        }

        /// <summary>
        /// Checks if request is AJAX
        /// </summary>
        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        /// <summary>
        /// Returns JsonResult with error message
        /// </summary>
        private JsonResult PrepareJsonResult(string exceptionInnerMsg)
        {
            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Message = $"JSON Ex: {exceptionInnerMsg}"
                }
            };
        }

        /// <summary>
        /// Returns ViewResult with exception
        /// </summary>
        private ViewResult PrepareNormalResult(ExceptionContext filterContext, string controllerName, string actionName)
        {
            HandleErrorInfo errorInfo = new HandleErrorInfo(filterContext.Exception.ExceptionFromInnerEx(), controllerName, actionName);

            return new ViewResult()
            {
                ViewName = "Error",
                TempData = filterContext.Controller.TempData,
                ViewData = new ViewDataDictionary<HandleErrorInfo>(errorInfo)
            };
        }
    }
}