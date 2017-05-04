using AutoMapper;
using KrisApp.DataModel.Contact;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Pages;
using KrisApp.DataModel.Results;
using KrisApp.Models.Me;
using KrisApp.Models.Pages;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    public class MeController : Controller
    {
        private readonly ILogger _log;
        private readonly IContactService _contactSrv;
        private readonly IMapper _mapper;
        private readonly IPageContentService _pageContentSrv;

        public MeController(ILogger log, IContactService contactSrv, IMapper mapper, IPageContentService pageContentSrv)
        {
            _log = log;
            _contactSrv = contactSrv;
            _mapper = mapper;
            _pageContentSrv = pageContentSrv;
        }

        //TODO: test jednostkowy
        public ActionResult About()
        {
            PageContent pageContent = _pageContentSrv.GetPageContentByCode(PageContent.Type.About.ToString());

            if (pageContent != null)
            {
                PageContentModel model = _mapper.Map<PageContentModel>(pageContent);
                return View((object)model.Content);
            }
            else
            {
                return RedirectToAction("AboutStatic");
            }
        }

        public ActionResult AboutStatic()
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
                "NUnit",
                "Moq",
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
                ContactMessage msg = _mapper.Map<ContactModel, ContactMessage>(model);

                Result result = _contactSrv.AddContactMessage(msg);
                // TODO: przenieść do klasy wraz z definicją typu klasy cssowej
                TempData["Msg"] = $"Wiadomość wysłana pomyślnie! Otrzymała ID = {result.Message}";
            }

            return RedirectToAction("Contact");
        }
    }
}