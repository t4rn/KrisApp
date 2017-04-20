using KrisApp.DataAccess;
using KrisApp.DataModel.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace KrisApp.Services
{
    public class DictionaryService : AbstractService
    {
        private readonly int cacheTimeMinutes = 360;

        private enum DictionaryTypes
        {
            User, Article
        }

        public DictionaryService(KrisLogger log) : base(log)
        {}

        /// <summary>
        /// Zwraca listę typów użytkowników - z cache albo z DB
        /// </summary>
        internal List<UserType> GetUserTypes()
        {
            List<UserType> userTypes = GetFromCacheOrDB(DictionaryTypes.User, () => GetUserTypesFromDB());

            return userTypes;
        }

        /// <summary>
        /// Zwraca listę typów artykułów - z cache albo z DB
        /// </summary>
        internal List<ArticleType> GetArticleTypes()
        {
            List<ArticleType> userTypes = GetFromCacheOrDB(DictionaryTypes.Article, () => GetArticleTypesFromDB());

            return userTypes;
        }

        /// <summary>
        /// Zwraca listę typow użytkowników z bazy danych
        /// </summary>
        private List<UserType> GetUserTypesFromDB()
        {
            List<UserType> userTypes = null;

            using (KrisDbContext context = new KrisDbContext())
            {
                userTypes = context.UserTypes.Where(x => x.Ghost == false).ToList();
            }

            return userTypes;
        }

        /// <summary>
        /// Zwraca listę typow artykułów z bazy danych
        /// </summary>
        private List<ArticleType> GetArticleTypesFromDB()
        {
            List<ArticleType> dict = null;

            using (KrisDbContext context = new KrisDbContext())
            {
                dict = context.ArticleTypes.AsNoTracking().Where(x => x.Ghost == false).ToList();
            }

            return dict;
        }

        /// <summary>
        /// Zawraca słownik z cache lub pobiera go z bazy, jeżeli nie było go w cache (i zapisuje w cache)
        /// </summary>
        private T GetFromCacheOrDB<T>(DictionaryTypes dictionaryType, Func<T> getItemFromDB) where T : class
        {
            T item = MemoryCache.Default.Get(dictionaryType.ToString()) as T;
            if (item == null)
            {
                item = getItemFromDB();
                MemoryCache.Default.Add(dictionaryType.ToString(), item, DateTime.Now.AddMinutes(cacheTimeMinutes));
            }
            return item;
        }
    }
}