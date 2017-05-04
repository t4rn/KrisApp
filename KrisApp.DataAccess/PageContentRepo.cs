using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Pages;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace KrisApp.DataAccess
{
    public class PageContentRepo : BaseDAL, IPageContentRepository
    {
        public PageContentRepo(string cs) : base(cs)
        {}

        public void Add(PageContent pageContent)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.PageContents.Add(pageContent);
                context.SaveChanges();
            }
        }

        public PageContent GetPageContentByCode(string code)
        {
            PageContent pageContent = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                pageContent = context.PageContents.AsNoTracking()
                    .Where(x => x.Code == code).FirstOrDefault();
            }

            return pageContent;
        }

        public async Task<List<PageContent>> GetPageContents()
        {
            List<PageContent> pageContents = new List<PageContent>();

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                pageContents = await context.PageContents.AsNoTracking()
                    .ToListAsync();
            }

            return pageContents;
        }

        public PageContent GetPageContents(int id)
        {
            PageContent pageContent = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                pageContent = context.PageContents.AsNoTracking()
                    .Where(x => x.ID == id).FirstOrDefault();
            }

            return pageContent;
        }

        public void UpdatePageContent(PageContent pageContent)
        {
            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                context.Entry(pageContent).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
