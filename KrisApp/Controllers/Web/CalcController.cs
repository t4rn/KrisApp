using KrisApp.DataModel.Interfaces;
using KrisApp.Models.Calc;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    public class CalcController : BaseController
    {
        private readonly ICalcService _calcService;

        public CalcController(ICalcService calcService)
        {
            _calcService = calcService;
        }

        public ActionResult B2b()
        {
            return View();
        }

        public ActionResult Uod()
        {
            UodModel model = new UodModel()
            {
                Limit = 42764,
                BruttoAmountPerMonth = 10000
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Uod(UodModel model)
        {
            model.NettoAmounts = _calcService.CalculateIncome(model.BruttoAmountPerMonth, model.Limit);
            return View(model);
        }
    }
}