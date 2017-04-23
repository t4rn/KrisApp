using AutoMapper;
using KrisApp.Common.Extensions;
using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Users;
using KrisApp.Models.Articles;
using KrisApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ILogger _log;
        private readonly IArticleService _articleSrv;
        private readonly IDictionaryService _dictSrv;
        private readonly IMapper _mapper;
        private readonly User _user;

        public ArticleController(ILogger log, IArticleService articleSrv, IDictionaryService dictSrv, IMapper mapper)
        {
            _log = log;
            _articleSrv = articleSrv;
            _dictSrv = dictSrv;
            _mapper = mapper;
            _user = SessionService.GetFromSession<User>(SessionService.SessionItem.User);
        }

        //public ActionResult Index()
        //{
        //    ArticleHomeModel model = _articleSrv.PrepareArticleHomeModel(3);

        //    return View(model);
        //}

        public ActionResult List(string id)
        {
            List<Article> articles = null;

            ArticleType.ArticleTypeCode articleTypeCode;
            if (!string.IsNullOrWhiteSpace(id) && 
                Enum.TryParse(id.ToUpper(), out articleTypeCode))
            {
                articles = _articleSrv.GetArticlesByType(articleTypeCode);
            }
            else
            {
                articles = _articleSrv.GetArticles();
            }

            ArticleListModel model = new ArticleListModel();
            model.Articles = _mapper.Map<List<ArticleDetailsModel>>(articles);
            model.IsMod = _user?.Type?.Code.In(UserType.UserTypeCodes.ADM.ToString(), UserType.UserTypeCodes.MOD.ToString());

            return View(model);
        }

        public ActionResult CreateArticle()
        {
            ArticleModel model = new ArticleModel();
            model.ArticleTypes = PrepareArticleTypes();

            ViewBag.Message = TempData["Msg"];

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateArticle(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                Article a = _mapper.Map<ArticleModel, Article>(model);
                Article article = _articleSrv.AddArticle(a);

                TempData["Msg"] = $"Artykuł dodany pomyślnie! Otrzymał ID = {article.Id}.";
            }

            return RedirectToAction("CreateArticle");
        }

        public ActionResult Details(int id)
        {
            Article article = _articleSrv.GetByID(id);
            if (article != null)
            {
                ArticleDetailsModel model = _mapper.Map<ArticleDetailsModel>(article);
                return View(model);
            }
            else
            {
                _log.Error("[Details] Brak artykulu o ID = '{0}'", id);
                return RedirectToAction("List");
            }
        }

        public ActionResult EditArticle(int id)
        {
            Article article = _articleSrv.GetByID(id);

            if (article != null)
            {
                ArticleModel model = _mapper.Map<ArticleModel>(article);
                model.ArticleTypes = PrepareArticleTypes();

                return View(model);
            }
            else
            {
                _log.Error("[EditArticle] Brak artykulu o ID = '{0}'", id);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditArticle(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                Article article = _mapper.Map<Article>(model);
                _articleSrv.UpdateArticle(article);
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// Zwraca SelectList z typami artykułów
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> PrepareArticleTypes()
        {
            List<ArticleType> articleTypes = _dictSrv.GetDictionary<ArticleType>().Where(x => x.IsMain == false).ToList();
            IEnumerable<SelectListItem> articleTypesSelectList = PrepareArticleTypesSelectItemList(articleTypes);
            return articleTypesSelectList;
        }

        /// <summary>
        /// Zwraca listę SelectListItem wypełnioną danymi z przekazanej listy typów artykułów
        /// </summary>
        private List<SelectListItem> PrepareArticleTypesSelectItemList(List<ArticleType> articleTypes)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            // TODO: automapper
            foreach (ArticleType at in articleTypes)
            {
                items.Add(new SelectListItem() { Value = at.ID.ToString(), Text = at.Name });
            }

            return items;
        }
    }
}