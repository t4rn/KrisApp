using KrisApp.Models.Nav;
using KrisApp.Services;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    public class NavController : Controller
    {
        private readonly KrisLogger _log;
        private readonly NavService _navSrv;

        public NavController()
        {
            _log = new KrisLogger();
            _navSrv = new NavService(_log);
        }

        public PartialViewResult MainMenu()
        {
            string controller = ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString();

            MenuModel model = _navSrv.PrepareMenuModel(controller);

            return PartialView(model);
        }
    }
}