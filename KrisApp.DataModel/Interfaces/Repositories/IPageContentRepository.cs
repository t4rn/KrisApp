using KrisApp.DataModel.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KrisApp.DataModel.Interfaces.Repositories
{
    public interface IPageContentRepository
    {
        Task <List<PageContent>> GetPageContents();
        PageContent GetPageContentByCode(string code);
        void Add(PageContent pageContent);
        PageContent GetPageContents(int id);
        void UpdatePageContent(PageContent pageContent);
    }
}
