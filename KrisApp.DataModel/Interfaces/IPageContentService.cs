using KrisApp.DataModel.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KrisApp.DataModel.Interfaces
{
    public interface IPageContentService
    {
        Task<List<PageContent>> GetPageContents();
        PageContent GetPageContentByCode(string code);
        void Add(PageContent pageContent);
        PageContent GetPageContent(int id);
        void UpdatePageContent(PageContent pageContent);
    }
}
