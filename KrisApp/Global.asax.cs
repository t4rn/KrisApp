using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KrisApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
            //.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //        GlobalConfiguration.Configuration.Formatters
            //            .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            AutofacConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
