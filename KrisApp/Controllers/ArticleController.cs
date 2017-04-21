using AutoMapper;
using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Enums;
using KrisApp.DataModel.Interfaces;
using KrisApp.Models.Articles;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly string _articleListView;
        private readonly ILogger _log;
        private readonly IArticleService _articleSrv;
        private readonly IDictionaryService _dictSrv;
        private readonly IMapper _mapper;

        public ArticleController(ILogger log, IArticleService articleSrv, IDictionaryService dictSrv, IMapper mapper)
        {
            _log = log;
            _articleSrv = articleSrv;
            _articleListView = "ArticleListPage";
            _dictSrv = dictSrv;
            _mapper = mapper;
        }

        //public ActionResult Index()
        //{
        //    ArticleHomeModel model = _articleSrv.PrepareArticleHomeModel(3);

        //    return View(model);
        //}

        public ActionResult List()
        {
            ArticleListModel model = new ArticleListModel();
            model.Articles = _articleSrv.GetArticles();

            return View(model);
        }

        public ActionResult Create()
        {
            ArticleCreateModel model = new ArticleCreateModel();

            List<ArticleType> articleTypes = _dictSrv.GetDictionary<ArticleType>().Where(x => x.IsMain == false).ToList();

            model.ArticleTypes = PrepareArticleTypesSelectItemList(articleTypes);

            ViewBag.Message = TempData["Msg"];

            return View(model);
        }

        /// <summary>
        /// Zwraca listę SelectListItem wypełnioną danymi z przekazanej listy typów artykułów
        /// </summary>
        private List<SelectListItem> PrepareArticleTypesSelectItemList(List<ArticleType> articleTypes)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (ArticleType at in articleTypes)
            {
                items.Add(new SelectListItem() { Value = at.ID.ToString(), Text = at.Name });
            }

            return items;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                Article a = _mapper.Map<ArticleModel, Article>(model);
                Article article = _articleSrv.AddArticle(a);

                TempData["Msg"] = $"Artykuł dodany pomyślnie! Otrzymał ID = {article.Id}.";
            }

            return RedirectToAction("Create");
        }

        public ActionResult Asp()
        {
            ArticleListModel model = PrepareArticleListModel(ArticleTypeEnum.ASP);
            return View(_articleListView, model);
        }

        public ActionResult Wcf()
        {
            ArticleListModel model = PrepareArticleListModel(ArticleTypeEnum.WCF);
            return View(_articleListView, model);
        }

        public ActionResult Pattern()
        {
            ArticleListModel model = PrepareArticleListModel(ArticleTypeEnum.PATTERN);
            return View(_articleListView, model);
        }

        public ActionResult Sql()
        {
            ArticleListModel model = PrepareArticleListModel(ArticleTypeEnum.SQL);
            return View(_articleListView, model);
        }

        private ArticleListModel PrepareArticleListModel(ArticleTypeEnum articleType)
        {
            ArticleListModel model = new ArticleListModel();
            model.Articles = _articleSrv.GetArticlesByType(articleType);
            model.ArticleType = model.Articles.FirstOrDefault()?.Type?.Name;

            return model;
        }

        public ActionResult Details(int id)
        {
            ArticlePageModel model = new ArticlePageModel();
            model.Article = _articleSrv.GetByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ArticlePageModel model = new ArticlePageModel();
            model.Article = _articleSrv.GetByID(id);

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