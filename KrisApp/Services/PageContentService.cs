using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace KrisApp.Services
{
    public class PageContentService : AbstractService, IPageContentService
    {
        private readonly IPageContentRepository _pageContentRepo;

        public PageContentService(ILogger log, IPageContentRepository pageContentRepo) : base(log)
        {
            _pageContentRepo = pageContentRepo;
        }

        public void Add(PageContent pageContent)
        {
            pageContent.AddDate = DateTime.Now;
            _pageContentRepo.Add(pageContent);
        }

        public PageContent GetPageContent(int id)
        {
            return _pageContentRepo.GetPageContents(id);
        }

        /// <summary>
        /// Zwraca zawartość strony o danym symbolu
        /// </summary>
        public PageContent GetPageContentByCode(string code)
        {
            //TODO: cache
            return _pageContentRepo.GetPageContentByCode(code);
        }

        /// <summary>
        /// Zwraca wszystkie statyczne zawartości stron
        /// </summary>
        public Task<List<PageContent>> GetPageContents()
        {
            return _pageContentRepo.GetPageContents();
        }

        public void UpdatePageContent(PageContent pageContent)
        {
            _pageContentRepo.UpdatePageContent(pageContent);
        }
    }
}