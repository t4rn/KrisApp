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
        /// Aktualizuje przekazany artykuł
        /// </summary>
        void UpdateArticle(Article article);

        /// <summary>
        /// Zwraca listę artykułów
        /// </summary>
        List<Article> GetArticles();

        /// <summary>
        /// Zwraca listę artykułów o danym typie
        /// </summary>
        List<Article> GetArticlesByType(ArticleType.ArticleTypeCode articleType);

        /// <summary>
        /// Zwraca listę typów artykułów
        /// </summary>
        List<ArticleType> GetArticleTypes();

        Article GetByCode(string code);
    }
}
