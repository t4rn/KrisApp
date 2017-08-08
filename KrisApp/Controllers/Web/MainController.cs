using System;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Error(Exception ex, string controllerName, string actionName)
        {
            HandleErrorInfo model = new HandleErrorInfo(ex, controllerName, actionName);
            return View(model);
        }
    }
}