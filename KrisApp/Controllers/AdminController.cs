using KrisApp.Models.Admin;
using KrisApp.Models.Articles;
using KrisApp.Services;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly KrisLogger _log;
        private readonly LogService _logSrv;
        private readonly ContactService _contactSrv;
        private readonly ArticleService _articleSrv;

        public AdminController()
        {
            _log = new KrisLogger();
            _logSrv = new LogService(_log);
            _contactSrv = new ContactService(_log);
            _articleSrv = new ArticleService(_log);
        }

        public ActionResult Logs()
        {
            LogsViewModel model = _logSrv.GetLogsAll();
            return View(model);
        }

        public ActionResult ContactMessages()
        {
            ContactsListModel model = _contactSrv.GetContactMessages();
            return View(model);
        }

        public ActionResult Articles()
        {
            ArticleListModel model = _articleSrv.PrepareArticleListModel();

            return View(model);
        }

    }
}