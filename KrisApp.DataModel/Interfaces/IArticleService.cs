using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Dictionaries;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces
{
    public interface IArticleService
    {
        Article AddArticle(Article article);

        Article GetByID(int id);

        /// <summary>
        /// Updates article
        /// </summary>
        void UpdateArticle(Article article);

        /// <summary>
        /// Returns all articles
        /// </summary>
        List<Article> GetArticles();

        /// <summary>
        /// Returns articles of a given type
        /// </summary>
        List<Article> GetArticlesByType(ArticleType.ArticleTypeCode articleType);

        /// <summary>
        /// Returns a list of article types
        /// </summary>
        List<ArticleType> GetArticleTypes();

        /// <summary>
        /// Returns an article by its unique code
        /// </summary>
        Article GetByCode(string code);

        /// <summary>
        /// Returns articles whose title contains a given part
        /// </summary>
        List<Article> GetArticlesByTitlePart(string titlePart);

        List<Article> GetArticles(string titlePart, string type);
    }
}
