using KrisApp.DataModel.Interfaces;
using KrisApp.Models.Admin;
using KrisApp.Models.Articles;
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

        public AdminController(ILogger log, IAppLogService appLogSrv, IContactService contactSrv, IArticleService articleSrv)
        {
            _log = log;
            _appLogSrv = appLogSrv;
            _contactSrv = contactSrv;
            _articleSrv = articleSrv;
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
            ArticleListModel model = new ArticleListModel();
            model.Articles =_articleSrv.GetArticles();

            return View(model);
        }
    }
}