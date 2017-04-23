using AutoMapper;
using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Interfaces;
using KrisApp.Models.Admin;
using KrisApp.Models.Articles;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger _log;
        private readonly IAppLogService _appLogSrv;
        private readonly IContactService _contactSrv;
        private readonly IArticleService _articleSrv;
        private readonly IMapper _mapper;

        public AdminController(ILogger log, IAppLogService appLogSrv, IContactService contactSrv, IArticleService articleSrv, IMapper mapper)
        {
            _log = log;
            _appLogSrv = appLogSrv;
            _contactSrv = contactSrv;
            _articleSrv = articleSrv;
            _mapper = mapper;
        }

        public ActionResult Logs()
        {
            LogsViewModel model = new LogsViewModel();

            model.AppLogs = _appLogSrv.GetLogsAll();
            return View(model);
        }

        public ActionResult ContactMessages()
        {
            ContactsListModel model = new ContactsListModel();

            model.ContactMessages = _contactSrv.GetContactMessages();

            return View(model);
        }

        public ActionResult Articles()
        {
            List<Article> articles = _articleSrv.GetArticles();
            ArticleListModel model = new ArticleListModel();
            model.Articles = _mapper.Map<List<ArticleDetailsModel>>(articles);;

            return View(model);
        }
    }
}