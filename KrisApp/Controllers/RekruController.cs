using AutoMapper;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Rekru;
using KrisApp.DataModel.Results;
using KrisApp.Models.Rekru;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    [Authorize]
    public class RekruController : Controller
    {
        private readonly ILogger _log;
        private readonly IRekruService _rekruSrv;
        private readonly IMapper _mapper;

        public RekruController(ILogger log, IRekruService rekruSrv, IMapper mapper)
        {
            _log = log;
            _rekruSrv = rekruSrv;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult List()
        {
            QuestionListModel model = new QuestionListModel();

            List<RekruQuestion> questions = _rekruSrv.GetQuestions();
            model.Questions = _mapper.Map<List<QuestionModel>>(questions);

            return View(model);
        }

        public ActionResult AddQuestion()
        {
            QuestionModel model = new QuestionModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestion(QuestionModel model)
        {
            if (ModelState.IsValid)
            {
                RekruQuestion q = _mapper.Map<RekruQuestion>(model);
                Result result = _rekruSrv.AddQuestion(q);
            }

            return RedirectToAction("List");
        }

        public ActionResult Details(int id)
        {
            RekruQuestion rq = _rekruSrv.GetQuestion(id);

            if (rq != null)
            {
                QuestionModel model = _mapper.Map<QuestionModel>(rq);

                return View(model);
            }
            else
            {
                return RedirectToAction("List");
            }
        }

        public ActionResult DetailsPrev(int id)
        {
            // TODO: porządnie
            return RedirectToAction("Details", routeValues: new { id = id - 1 });
        }

        public ActionResult DetailsNext(int id)
        {
            // TODO: porządnie
            return RedirectToAction("Details", routeValues: new { id = id + 1 });
        }

        public ActionResult EditQuestion(int id)
        {
            RekruQuestion question = _rekruSrv.GetQuestion(id);
            QuestionModel model = _mapper.Map<QuestionModel>(question);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditQuestion(QuestionModel model)
        {
            if (ModelState.IsValid)
            {
                RekruQuestion question = _mapper.Map<RekruQuestion>(model);
                // TODO: obsłużyć result
                Result result = _rekruSrv.EditQuestion(question);
            }

            return RedirectToAction("Details", routeValues: new { id = model.ID });
        }

        public ActionResult AddAnswer(int id)
        {
            AnswerModel model = new AnswerModel() { QuestionID = id };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnswer(AnswerModel model)
        {
            if (ModelState.IsValid)
            {
                RekruAnswer answer = _mapper.Map<RekruAnswer>(model);
                Result result = _rekruSrv.AddAnswer(answer);
            }

            return RedirectToAction("Details", new { id = model.QuestionID });
        }

        public ActionResult EditAnswer(int id)
        {
            RekruAnswer answer = _rekruSrv.GetAnswer(id);

            AnswerModel model = _mapper.Map<AnswerModel>(answer);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAnswer(AnswerModel model)
        {
            RekruAnswer answer = _mapper.Map<RekruAnswer>(model);

            Result result = _rekruSrv.EditAnswer(answer);


            return RedirectToAction("Details", routeValues: new { id = model.QuestionID });
        }
    }
}