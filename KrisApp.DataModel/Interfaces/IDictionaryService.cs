using KrisApp.DataModel.Dictionaries;
using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces
{
    public interface IDictionaryService
    {
        /// <summary>
        /// Generyczna metoda zwracająca słownik z cache lub DB
        /// </summary>
        List<T> GetDictionary<T>() where T : DictionaryItem;
    }
}
