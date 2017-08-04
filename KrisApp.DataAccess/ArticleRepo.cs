using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace KrisApp.DataAccess
{
    public class ArticleRepo : BaseDAL, IArticleRepository
    {
        public ArticleRepo(string cs) : base(cs)
        { }

        /// <summary>
        /// Adds article do DB
        /// </summary>
        public void AddArticle(Article article)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.Articles.Add(article);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns all articles with Type included
        /// </summary>
        public List<Article> GetArticles()
        {
            List<Article> articles = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                articles = context.Articles.AsNoTracking()
                    .Include(x => x.Type)
                    .ToList();
            }

            return articles;
        }

        /// <summary>
        /// Returns articles whose title contains a given part
        /// </summary>
        public List<Article> GetArticlesByTitlePart(string titlePart)
        {
            List<Article> articles = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                articles = context.Articles.AsNoTracking()
                    .Where(r => r.Title.ToUpper().Contains(titlePart.ToUpper()))
                    .Include(x => x.Type)
                    .ToList();
            }

            return articles;
        }

        public List<Article> GetArticlesByTitlePartAndType(string titlePart, string typeCode)
        {
            List<Article> articles = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                articles = context.Articles.AsNoTracking()
                    .Where(r => r.Type.Code == typeCode &&
                        r.Title.ToUpper().Contains(titlePart.ToUpper()))
                    .Include(x => x.Type)
                    .ToList();
            }

            return articles;
        }

        /// <summary>
        /// Returns articles of a given type
        /// </summary>
        public List<Article> GetArticlesByType(string typeCode)
        {
            List<Article> articles = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                articles = context.Articles.AsNoTracking()
                    .Where(x => x.Type.Code == typeCode)
                    .Include(x => x.Type)
                    .ToList();
            }

            return articles;
        }

        public Article GetByCode(string code)
        {
            Article a = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                a = context.Articles.AsNoTracking()
                    .Where(x => x.Code == code)
                    .Include(x => x.Type)
                    .FirstOrDefault();
            }

            return a;
        }

        /// <summary>
        /// Returns an article with a given ID
        /// </summary>
        public Article GetByID(int id)
        {
            Article a = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                a = context.Articles.AsNoTracking()
                    .Where(x => x.Id == id)
                    .Include(x => x.Type)
                    .FirstOrDefault();
            }

            return a;
        }

        /// <summary>
        /// Updates given article
        /// </summary>
        public void UpdateArticle(Article article)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                //context.Articles.Attach(article);
                context.Entry(article).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
