using KrisApp.DataModel.Interfaces;
using KrisApp.Models.Nav;
using KrisApp.Services;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    public class NavController : Controller
    {
        private readonly ILogger _log;
        private readonly NavService _navSrv;

        public NavController(ILogger log, IArticleService articleSrv, ISessionService sessionSrv)
        {
            _log = log;
            _navSrv = new NavService(_log, articleSrv, sessionSrv);
        }

        public PartialViewResult MainMenu()
        {
            string controller = ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString();

            MenuModel model = _navSrv.PrepareMenuModel(controller);

            return PartialView(model);
        }
    }
}