using KrisApp.DataModel.Results;
using KrisApp.Models.Me;
using KrisApp.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    public class MeController : Controller
    {
        private readonly KrisLogger _log;
        private readonly ContactService _contactSrv;

        public MeController()
        {
            _log = new KrisLogger();
            _contactSrv = new ContactService(_log);
        }

        public ActionResult About()
        {
            AboutMeModel model = new AboutMeModel();

            model.MyTechnologies = new List<string>()
            {
                ".NET", "C#", "ASP.NET Web Forms", "ASP.NET MVC", "Web Services", "WCF", "WebAPI", "Windows Forms", "Windows Services", "HTML", "CSS", "XML", "JSON",
                "Entity Framework", "Fluent NHibernate", "Dapper", "LINQ",
                //"JavaScript", "jQuery", "AngularJS", "SignalR",
                "MS SQL", "PostgreSQL",
                "NUnit", "NLog", "Ninject", "Moq", "Autofac", "AutoMapper",
                "SoapUI", "Postman", "Fiddler",
                "SVN", "GIT", "IIS", "Redmine", "Jira", "Jenkins"

                /*
                 * .NET, C#, JavaScript, HTML, CSS, SCSS, XML, JSON, YAML, ASP.NET MVC, WPF, Console Apps, Windows Phone, Silverlight, WinForms, Entity Framework, Dapper, NHibernate, LINQ, Autofac, AJAX, jQuery, KnockoutJS, AngularJS, AureliaJS, NodeJS, KendoUI, WCF, NancyFX, WebAPI, ServiceStack, RESTful API, SignalR, nopCommerce, Azure, Azure Websites, Azure Infrastructure, AWS, Windows Server, SQL, MSSQL, MongoDB, Redis, PostgreSQL, FIX, OSM, IIS, GIT, TFS, Docker, Visual Studio, Microsoft SQL Server Management Studio, Expression Blend, ReSharper, TeamCity, Selenium, NUnit, SpecsFor, Unit Testing, Integration Testing, End-To-End Testing, Responsive Design, Powershell, DevOps, DDD, CQRS, Message bus, Consulting, Agile, UML, Documentation, Design Patterns, Clean Code, SOLID, Refactoring, Async, Parallel, Multithreading, Profiling, Performance tuning
                 */
            };

            return View(model);
        }

        public ActionResult Website()
        {
            WebsiteDescModel model = new WebsiteDescModel();
            model.UsedTechnologies = new List<string>()
            {
                ".NET Framework 4.5",
                "ASP.NET MVC 5",
                "ASP.NET Web API 2",
                "Entity Framework 6",
                "MemoryCache",
                "AutoMapper",
                "Autofac",
                "MS SQL",
                "Bootstrap 3"
            };
            return View(model);
        }

        public ActionResult Contact()
        {
            ContactModel model = new ContactModel();
            ViewBag.Message = TempData["Msg"];

            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                Result result = _contactSrv.AddContactMessage(model);
                // TODO: przenieść do klasy wraz z definicją typu klasy cssowej
                TempData["Msg"] = $"Wiadomość wysłana pomyślnie! Otrzymała ID = {result.Message}";
            }

            return RedirectToAction("Contact");
        }
    }
}