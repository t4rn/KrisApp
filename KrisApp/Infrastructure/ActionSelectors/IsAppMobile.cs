using System.Reflection;
using System.Web.Mvc;

namespace KrisApp.Infrastructure.ActionSelectors
{
    public class IsAppMobile : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.Headers["x-AppMobile"] != null;
        }
    }
}