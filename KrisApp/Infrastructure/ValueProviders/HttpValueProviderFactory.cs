using System.Web.Mvc;

namespace KrisApp.Infrastructure.ValueProviders
{
    public class HttpValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext.Controller.GetType() == typeof(Controllers.UserController) &&
                controllerContext.RouteData.Values["action"].ToString() == "Register")
            {
                return new HttpValueProvider(controllerContext.HttpContext.Request.Headers);
            }
            else
            {
                return null;
            }
        }
    }
}