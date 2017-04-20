using KrisApp.DataModel.Dictionaries;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces
{
    public interface IDictionaryRepository<T> where T : class
    {
        List<T> GetItems();
    }
}
