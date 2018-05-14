using AutoMapper;
using KrisApp.DataModel.Calc;
using KrisApp.DataModel.Interfaces;
using KrisApp.Models.Calc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    public class CalcController : BaseController
    {
        private readonly ICalcService _calcService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;

        public CalcController(ICalcService calcService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _calcService = calcService;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        public ActionResult B2b()
        {
            return View();
        }

        public ActionResult Uod()
        {
            var summariesFromSession = _sessionService.GetFromSession<List<UodSummaryModel>>(SessionItem.Uod);

            UodModel model = new UodModel()
            {
                Limit = 85528,
                BruttoAmountPerMonth = 10000,
                SavedSummaries = summariesFromSession
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Uod(UodModel model)
        {
            if (ModelState.IsValid)
            {
                UodSummary uodSummary = _calcService.CalculateIncome(model.BruttoAmountPerMonth, model.Limit);

                model.CurrentSummary = _mapper.Map<UodSummaryModel>(uodSummary);

                var summariesFromSession = _sessionService.GetFromSession<List<UodSummaryModel>>(SessionItem.Uod);

                if (summariesFromSession == null)
                {
                    summariesFromSession = new List<UodSummaryModel>() { model.CurrentSummary };
                    _sessionService.AddToSession(SessionItem.Uod, summariesFromSession);
                }
                else if (!summariesFromSession.Exists(x => Convert.ToDecimal(x.Brutto) == model.BruttoAmountPerMonth))
                {
                    summariesFromSession.Add(model.CurrentSummary);
                }

                model.SavedSummaries = summariesFromSession;

                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(Uod));
            }
        }
    }
}