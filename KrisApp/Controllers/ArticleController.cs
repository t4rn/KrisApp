using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Enums;
using KrisApp.Models.Articles;
using KrisApp.Services;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly KrisLogger _log;
        private readonly ArticleService _articleSrv;
        private readonly string _articleListView;

        public ArticleController()
        {
            _log = new KrisLogger();
            _articleSrv = new ArticleService(_log);
            _articleListView = "ArticleListPage";
        }
        public ActionResult Index()
        {
            ArticleHomeModel model = _articleSrv.PrepareArticleHomeModel(3);

            return View(model);
        }

        public ActionResult List()
        {
            ArticleListModel model = _articleSrv.PrepareArticleListModel();

            return View(model);
        }

        public ActionResult Create()
        {
            ArticleCreateModel model = _articleSrv.PrepareArticleCreateModel();

            ViewBag.Message = TempData["Msg"];

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                Article article = _articleSrv.AddArticle(model);

                TempData["Msg"] = $"Artykuł dodany pomyślnie! Otrzymał ID = {article.Id}.";
            }

            return RedirectToAction("Create");
        }

        public ActionResult Asp()
        {
            ArticleListModel model = _articleSrv.PrepareArticleListModel(ArticleTypeEnum.ASP);
            return View(_articleListView, model);
        }

        public ActionResult Wcf()
        {
            ArticleListModel model = _articleSrv.PrepareArticleListModel(ArticleTypeEnum.WCF);
            return View(_articleListView, model);
        }

        public ActionResult Pattern()
        {
            ArticleListModel model = _articleSrv.PrepareArticleListModel(ArticleTypeEnum.PATTERN);
            return View(_articleListView, model);
        }

        public ActionResult Sql()
        {
            ArticleListModel model = _articleSrv.PrepareArticleListModel(ArticleTypeEnum.SQL);
            return View(_articleListView, model);
        }

        public ActionResult Details(int id)
        {
            ArticlePageModel model = _articleSrv.PrepareArticlePageModel(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ArticlePageModel model = _articleSrv.PrepareArticlePageModel(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                _articleSrv.UpdateArticle(article);
            }

            return RedirectToAction("Index");
        }
    }
}