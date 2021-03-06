﻿using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Users;
using KrisApp.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KrisApp.Services
{
    public class ArticleService : AbstractService, IArticleService
    {
        private readonly IDictionaryService _dictSrv;
        private readonly IArticleRepository _articleRepo;
        private readonly User _user;
        private readonly ISessionService _sessionSrv;

        public ArticleService(ILogger log, IArticleRepository articleRepo, IDictionaryService dictSrv, ISessionService sessionSrv) : base(log)
        {
            _dictSrv = dictSrv;
            _articleRepo = articleRepo;
            _sessionSrv = sessionSrv;
            _user = _sessionSrv.GetFromSession<User>(SessionItem.User);
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
        public Article AddArticle(Article article)
        {
            _log.Debug("[AddArticle] Start - type '{0}' code = '{1}' author '{2}' title '{3}' length '{4}'",
                article.TypeId, article.Code, article.Author, article.Title, article.Content.Length);

            article.AddDate = DateTime.Now;
            article.Author = _user?.Login ?? article.Author;

            _articleRepo.AddArticle(article);

            return article;
        }

        /// <summary>
        /// Zwraca artykuł o danym ID
        /// </summary>
        public Article GetByID(int id)
        {
            return _articleRepo.GetByID(id);
        }

        public Article GetByCode(string code)
        {
            return _articleRepo.GetByCode(code);
        }

        /// <summary>
        /// Aktualizuje przekazany artykuł
        /// </summary>
        public void UpdateArticle(Article article)
        {
            _articleRepo.UpdateArticle(article);
        }

        /// <summary>
        /// Zwraca model dla strony z listą wszystkich artykułów
        /// </summary>
        public List<Article> GetArticles()
        {
            return _articleRepo.GetArticles()
                    .OrderByDescending(x => x.Id)
                    .ToList();
        }

        /// <summary>
        /// Zwraca listę artykułów o danym typie
        /// </summary>
        public List<Article> GetArticlesByType(ArticleType.ArticleTypeCode articleType)
        {
            return _articleRepo.GetArticlesByType(articleType.ToString());
        }

        /// <summary>
        /// Zwraca listę typów artykułów
        /// </summary>
        public List<ArticleType> GetArticleTypes()
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

        public List<Article> GetArticlesByTitlePart(string titlePart)
        {
            return _articleRepo.GetArticlesByTitlePart(titlePart);
        }

        public List<Article> GetArticles(string titlePart, string typeCode)
        {
            List<Article> articles = null;

            ArticleType.ArticleTypeCode articleTypeCode;
            if (!string.IsNullOrWhiteSpace(typeCode) &&
                Enum.TryParse(typeCode.ToUpper(), out articleTypeCode))
            {
                // by Type
                if (!string.IsNullOrWhiteSpace(titlePart))
                {
                    articles = _articleRepo.GetArticlesByTitlePartAndType(titlePart, typeCode);
                }
                else
                {
                    articles = GetArticlesByType(articleTypeCode);
                }
            }
            else
            {
                // all
                if (!string.IsNullOrWhiteSpace(titlePart))
                {
                    articles = GetArticlesByTitlePart(titlePart);
                }
                else
                {
                    articles = GetArticles();
                }
            }

            return articles;
        }
    }
}