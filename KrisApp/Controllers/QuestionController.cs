using AutoMapper;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Questions;
using KrisApp.DataModel.Results;
using KrisApp.Models.Questions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly ILogger _log;
        private readonly IQuestionService _questionSrv;
        private readonly IMapper _mapper;

        public QuestionController(ILogger log, IQuestionService questionSrv, IMapper mapper)
        {
            _log = log;
            _questionSrv = questionSrv;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            QuestionListModel model = new QuestionListModel();

            List<RekruQuestion> questions = _questionSrv.GetQuestions();
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
            RekruQuestion q = null;
            if (ModelState.IsValid)
            {
                q = _mapper.Map<RekruQuestion>(model);
                Result result = _questionSrv.AddQuestion(q);
                if (result.IsOK)
                {
                    return RedirectToAction("Details", routeValues: new { id = q.ID });
                }
                else
                {
                    return RedirectToAction("AddQuestion");
                }
            }
            else
            {
                return RedirectToAction("List");
            }
        }

        public ActionResult Details(int id)
        {
            RekruQuestion rq = _questionSrv.GetQuestion(id);

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
            RekruQuestion question = _questionSrv.GetQuestion(id);
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
                Result result = _questionSrv.EditQuestion(question);
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
        [ValidateInput(false)]
        public ActionResult AddAnswer(AnswerModel model)
        {
            if (ModelState.IsValid)
            {
                RekruAnswer answer = _mapper.Map<RekruAnswer>(model);
                Result result = _questionSrv.AddAnswer(answer);
            }

            return RedirectToAction("Details", new { id = model.QuestionID });
        }

        public ActionResult EditAnswer(int id)
        {
            RekruAnswer answer = _questionSrv.GetAnswer(id);

            AnswerModel model = _mapper.Map<AnswerModel>(answer);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditAnswer(AnswerModel model)
        {
            RekruAnswer answer = _mapper.Map<RekruAnswer>(model);

            Result result = _questionSrv.EditAnswer(answer);


            return RedirectToAction("Details", routeValues: new { id = model.QuestionID });
        }
    }
}