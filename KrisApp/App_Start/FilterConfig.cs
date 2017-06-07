using KrisApp.DataModel.Interfaces;
using KrisApp.Infrastructure.ExceptionFilters;
using System.Web.Mvc;

namespace KrisApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(DependencyResolver.Current.GetService<ExcepionLoggingFilter>());
        }
    }
}
