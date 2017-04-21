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

        public ActionResult List()
        {
            QuestionListModel model = new QuestionListModel();

            List<RekruQuestion> questions = _rekruSrv.GetQuestions();
            model.Questions = _mapper.Map<List<QuestionModel>>(questions);

            return View(model);
        }

        public ActionResult Add()
        {
            QuestionAddModel model = new QuestionAddModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(QuestionAddModel model)
        {
            if (ModelState.IsValid)
            {
                RekruQuestion q = _mapper.Map<RekruQuestion>(model);
                Result result = _rekruSrv.AddQuestion(q);
            }

            return RedirectToAction("Add");
        }
    }
}