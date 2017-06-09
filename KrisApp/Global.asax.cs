using KrisApp.Controllers;
using KrisApp.DataAccess;
using KrisApp.DataModel.Interfaces;
using KrisApp.Infrastructure.ModelBinders;
using KrisApp.Services;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KrisApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILogger _log;

        public MvcApplication()
        {
            _log = new KrisLogger(new AppLogRepo(Properties.Settings.Default.csDB));
        }
        protected void Application_Start()
        {
            //        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
            //.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //        GlobalConfiguration.Configuration.Formatters
            //            .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            //if (!string.IsNullOrEmpty(Properties.Settings.Default.ActiveTheme))
            //{
            //    string activeTheme = Properties.Settings.Default.ActiveTheme;
            //    ViewEngines.Engines.Insert(0, new ThemeViewEngine(activeTheme));
            //}

            //ModelBinderProviders.BinderProviders.Insert(0, new XmlModelBinderProvider());
            ModelBinderProviders.BinderProviders.Add(new XmlModelBinderProvider());
            //ModelBinders.Binders.Add(typeof(ContactModel), new XmlModelBinder());

            AutofacConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext httpContext = application.Context;
            HttpRequest request = httpContext?.Request;
            Exception ex = Server.GetLastError();

            string currentUser = httpContext?.User?.Identity?.Name;
            string controller = request?.RequestContext?.RouteData?.Values["controller"].ToString();
            string action = request?.RequestContext?.RouteData?.Values["action"].ToString();

            _log.Error("Global Ex in '{0}/{1}' user '{2}': {3}",
                controller, action, currentUser, ex.ToString());

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Main");
            routeData.Values.Add("action", "Error");
            routeData.Values.Add("ex", ex);
            routeData.Values.Add("controllerName", controller);
            routeData.Values.Add("actionName", action);

            IController errorController = new MainController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            Server.ClearError();

            //Response.RedirectToRoute("Error", new { msg = ex.Message });
        }
    }
}
