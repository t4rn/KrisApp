using KrisApp.Infrastructure;
using System.Collections;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    public abstract class BaseController : Controller
    {
        public ActionResult XML(object model)
        {
            return new XMLResult(model);
        }

        public ActionResult CSV(IEnumerable model, string fileName)
        {
            return new CSVResult(model, fileName);
        }
    }
}