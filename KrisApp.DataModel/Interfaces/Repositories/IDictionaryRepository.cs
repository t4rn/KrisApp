using KrisApp.DataModel.Dictionaries;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IDictionaryRepository
    {
        List<T> GetItems<T>() where T : DictionaryItem;
    }
}
