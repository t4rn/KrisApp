using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using KrisApp.DataModel.Dictionaries;

namespace KrisApp.DataAccess
{
    public class ArticleRepo : BaseDAL, IArticleRepository
    {
        public ArticleRepo(string cs) : base(cs)
        { }

        /// <summary>
        /// Zapisuje na bazie przekazany artykuł
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
        /// Zwraca wszystkie artykuły z includowanym Typem
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
        /// Zwraca artykuły o danym typie
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

        /// <summary>
        /// Zwraca artykuł o danym ID
        /// </summary>
        public Article GetByID(int id)
        {
            Article a = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                a = context.Articles.AsNoTracking()
                    .Where(x => x.Id == id).FirstOrDefault();
            }

            return a;
        }

        /// <summary>
        /// Aktualizuje przekazany artykuł
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
