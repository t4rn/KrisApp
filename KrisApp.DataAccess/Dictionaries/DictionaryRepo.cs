using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace KrisApp.DataAccess.Dictionaries
{
    public class DictionaryRepo : BaseDAL, IDictionaryRepository
    {
        public DictionaryRepo(string cs) : base(cs)
        {
        }

        public List<T> GetItems<T>() where T: DictionaryItem
        {
            List<T> items = null;
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                items = context.Set<T>().AsNoTracking().ToList();
            }

            return items;
        }
    }
}
