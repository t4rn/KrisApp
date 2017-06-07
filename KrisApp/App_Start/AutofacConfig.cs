using Autofac;
using Autofac.Integration.Mvc;
using KrisApp.AutofacModules;
using KrisApp.Infrastructure.ExceptionFilters;
using System.Web.Mvc;

namespace KrisApp
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);

            builder.RegisterFilterProvider();

            //builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterModule(new AutofacModule(Properties.Settings.Default.csDB));
            builder.RegisterModule(new AutoMapperModule());

            builder.RegisterType<ExcepionLoggingFilter>().InstancePerRequest();

            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}