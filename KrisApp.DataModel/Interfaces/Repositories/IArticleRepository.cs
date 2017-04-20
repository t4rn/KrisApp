using KrisApp.DataModel.Articles;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IArticleRepository
    {
        void AddArticle(Article article);

        Article GetByID(int id);
        void UpdateArticle(Article article);
        List<Article> GetArticles();
        List<Article> GetArticlesByType(string v);
    }
}
