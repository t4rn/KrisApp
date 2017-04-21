using KrisApp.DataAccess;
using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Enums;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KrisApp.Services
{
    public class ArticleService : AbstractService
    {
        private readonly DictionaryService _dictSrv;
        private readonly IArticleRepository _articleRepo;

        public ArticleService(KrisLogger log) : base(log)
        {
            _dictSrv = new DictionaryService(log);
            _articleRepo = new ArticleRepo(Properties.Settings.Default.csDB);
        }

        /// <summary>
        /// Zwraca model dla strony głównej z artykułami
        /// </summary>
        internal ArticleHomeModel PrepareArticleHomeModel(int sidArticlesColumnCount)
        {
            ArticleHomeModel model = new ArticleHomeModel();

            List<ArticleType> articleTypes = _dictSrv.GetDictionary<ArticleType>();
            List<ArticleTypeModel> articlesAll = PrepareArticleTypes(articleTypes);

            model.MainArticle = articlesAll.FirstOrDefault(x => x.IsMain == true);
            model.SideArticles = new List<List<ArticleTypeModel>>();

            List<ArticleTypeModel> sideArticles = articlesAll.Where(x => x.IsMain == false).ToList();

            for (int i = 0; i < sideArticles.Count; i += sidArticlesColumnCount)
            {
                model.SideArticles.Add(
                    sideArticles.GetRange(i, Math.Min(sidArticlesColumnCount, sideArticles.Count - i)));
            }

            return model;
        }

        /// <summary>
        /// Dodaje na bazę przekazany artykuł
        /// </summary>
        internal Article AddArticle(ArticleModel model)
        {
            _log.Debug("[AddArticle] Start - type '{0}', author '{1}' title '{2}' length '{3}'",
                model.TypeId, model.Author, model.Title, model.Content.Length);

            Article a = PrepareArticleFromModel(model);

            _articleRepo.AddArticle(a);

            return a;
        }

        /// <summary>
        /// Zwraca Article wypełniony danymi z przekazanego ArticleModel
        /// </summary>
        private Article PrepareArticleFromModel(ArticleModel model)
        {
            Article article = new Article();
            article.Author = model.Author;
            article.Title = model.Title;
            article.Content = model.Content;
            article.TypeId = model.TypeId;
            article.AddDate = DateTime.Now;

            return article;
        }

        /// <summary>
        /// Zwraca model zwierający artykuł o danym ID
        /// </summary>
        internal ArticlePageModel PrepareArticlePageModel(int id)
        {
            ArticlePageModel model = new ArticlePageModel();
            model.Article = _articleRepo.GetByID(id);

            return model;
        }

        /// <summary>
        /// Aktualizuje przekazany artykuł
        /// </summary>
        internal void UpdateArticle(Article article)
        {
            _articleRepo.UpdateArticle(article);
        }

        /// <summary>
        /// Zwraca model ArticleCreateModel wypełniony listą dostępnych typów artykułów
        /// </summary>
        internal ArticleCreateModel PrepareArticleCreateModel()
        {
            ArticleCreateModel model = new ArticleCreateModel();
            //model.ArticleToCreate = new ArticleModel();

            List<ArticleType> articleTypes;
            articleTypes = _dictSrv.GetDictionary<ArticleType>().Where(x => x.IsMain == false).ToList();

            model.ArticleTypes = PrepareArticleTypesSelectItemList(articleTypes);


            return model;
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

        /// <summary>
        /// Zwraca model dla strony z listą wszystkich artykułów
        /// </summary>
        internal ArticleListModel PrepareArticleListModel()
        {
            ArticleListModel model = new ArticleListModel();
            model.Articles = _articleRepo.GetArticles()
                    .OrderByDescending(x => x.Id)
                    .ToList();

            return model;
        }

        /// <summary>
        /// Zwraca model dla strony z listą artykułów o danym typie
        /// </summary>
        internal ArticleListModel PrepareArticleListModel(ArticleTypeEnum articleType)
        {
            ArticleListModel model = new ArticleListModel();
            model.Articles = _articleRepo.GetArticlesByType(articleType.ToString());
            model.ArticleType = model.Articles.FirstOrDefault()?.Type?.Name;

            return model;
        }

        /// <summary>
        /// Zwraca listę typów artykułów
        /// </summary>
        internal List<ArticleType> GetArticleTypes()
        {
            return _dictSrv.GetDictionary<ArticleType>();
        }

        private List<ArticleTypeModel> PrepareArticleTypes(List<ArticleType> articleTypes)
        {
            List<ArticleTypeModel> model = new List<ArticleTypeModel>();

            foreach (ArticleType at in articleTypes)
            {
                ArticleTypeModel m = new ArticleTypeModel();

                m.Description = at.Description;
                m.Title = at.Name;
                m.Code = at.Code.ToLower();
                m.IsMain = at.IsMain;

                model.Add(m);
            }

            return model;
        }
    }
}