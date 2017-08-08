using AutoMapper;
using KrisApp.DataModel.Contact;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Pages;
using KrisApp.DataModel.Results;
using KrisApp.Models.Me;
using KrisApp.Models.Pages;
using System.Diagnostics;
using System.Web.Mvc;
using System;

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

        //TODO: unit test
        public ActionResult About()
        {
            string methodName = new StackTrace().GetFrame(0).GetMethod().Name;

            return GenerateResult(methodName);
        }

        public ActionResult Website()
        {
            string methodName = new StackTrace().GetFrame(0).GetMethod().Name;

            return GenerateResult(methodName);
        }

        public ActionResult Tools()
        {
            string methodName = new StackTrace().GetFrame(0).GetMethod().Name;

            return GenerateResult(methodName);
        }

        private ActionResult GenerateResult(string methodName)
        {
            PageContent pageContent = _pageContentSrv.GetPageContentByCode(methodName);

            if (pageContent != null)
            {
                PageContentModel model = _mapper.Map<PageContentModel>(pageContent);
                return View((object)model.Content);
            }
            else
            {
                return RedirectToAction("Index", "Main");
            }
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
                // TODO: move to a seperate class with css class definition
                TempData["Msg"] = $"Wiadomość wysłana pomyślnie! Otrzymała ID = {result.Message}";
            }

            return RedirectToAction("Contact");
        }
    }
}