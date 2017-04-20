using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace KrisApp.DataAccess
{
    public class ArticleTypeRepo : BaseDAL, IArticleTypeRepository
    {
        public ArticleTypeRepo(string cs) : base(cs)
        {
        }

        /// <summary>
        /// Zwraca niezduchowane typy artykułów
        /// </summary>
        public List<ArticleType> GetArticleTypes()
        {
            List<ArticleType> articleTypes = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                articleTypes = context.ArticleTypes.AsNoTracking()
                    .Where(x => x.Ghost == false).ToList();
            }

            return articleTypes;
        }
    }
}
