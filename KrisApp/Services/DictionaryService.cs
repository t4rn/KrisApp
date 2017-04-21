using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace KrisApp.Services
{
    public class DictionaryService : AbstractService, IDictionaryService
    {
        private readonly int cacheTimeMinutes = 360;
        private readonly IDictionaryRepository _dictRepo;

        private enum DictionaryTypes
        {
            User, Article,
            Position,
            Skill
        }

        public DictionaryService(ILogger log, IDictionaryRepository dictRepo) : base(log)
        {
            _dictRepo = dictRepo;
        }

        /// <summary>
        /// Generyczna metoda zwracająca słownik z cache lub DB
        /// </summary>
        public List<T> GetDictionary<T>() where T : DictionaryItem
        {
            List<T> dictionaryItems = GetFromCacheOrDB(typeof(T).ToString(), () => GetDictFromDB<T>());

            return dictionaryItems;
        }

        /// <summary>
        /// Zwraca listę typow użytkowników z bazy danych
        /// </summary>
        private List<T> GetDictFromDB<T>() where T : DictionaryItem
        {
            List<T> userTypes = _dictRepo.GetItems<T>().Where(x => x.Ghost == false).ToList();

            return userTypes;
        }

        /// <summary>
        /// Zawraca słownik z cache lub pobiera go z bazy, jeżeli nie było go w cache (i zapisuje w cache)
        /// </summary>
        private T GetFromCacheOrDB<T>(string type, Func<T> getItemFromDB) where T : class
        {
            T item = MemoryCache.Default.Get(type) as T;
            if (item == null)
            {
                item = getItemFromDB();
                MemoryCache.Default.Add(type, item, DateTime.Now.AddMinutes(cacheTimeMinutes));
            }
            return item;
        }
    }
}