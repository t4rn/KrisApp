using AutoMapper;
using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Interfaces;
using KrisApp.DataModel.Pages;
using KrisApp.Infrastructure;
using KrisApp.Models.Admin;
using KrisApp.Models.Articles;
using KrisApp.Models.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KrisApp.Controllers
{
    [Authorize]
    //[LogActionFilter(Desc = "Opis z kontrolera")]
    public class AdminController : Controller
    {
        private readonly ILogger _log;
        private readonly IAppLogService _appLogSrv;
        private readonly IContactService _contactSrv;
        private readonly IArticleService _articleSrv;
        private readonly IMapper _mapper;
        private readonly IPageContentService _pageContentSrv;

        public AdminController(ILogger log, IAppLogService appLogSrv, IContactService contactSrv, 
            IArticleService articleSrv, IMapper mapper, IPageContentService pageContentSrv)
        {
            _log = log;
            _appLogSrv = appLogSrv;
            _contactSrv = contactSrv;
            _articleSrv = articleSrv;
            _mapper = mapper;
            _pageContentSrv = pageContentSrv;
        }

        public ActionResult Logs()
        {
            LogsViewModel model = new LogsViewModel();

            model.AppLogs = _appLogSrv.GetLogsAll();
            return View(model);
        }

        public ActionResult ContactMessages()
        {
            ContactsListModel model = new ContactsListModel();

            model.ContactMessages = _contactSrv.GetContactMessages();

            return View(model);
        }

        public ActionResult Articles()
        {
            List<Article> articles = _articleSrv.GetArticles();
            ArticleListModel model = new ArticleListModel();
            model.Articles = _mapper.Map<List<ArticleDetailsModel>>(articles);

            return View(model);
        }

        //TODO: przenieść do oddzielnego kontrolera
        public async Task<ViewResult> PageContents()
        {
            List<PageContent> pageContents = await _pageContentSrv.GetPageContents();

            List<PageContentModel> model = _mapper.Map<List<PageContentModel>>(pageContents);

            return View(model);
        }

        public ViewResult CreatePageContent()
        {
            PageContentModel model = new PageContentModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreatePageContent(PageContentModel model)
        {
            if (ModelState.IsValid)
            {
                PageContent pageContent = _mapper.Map<PageContent>(model);

                _pageContentSrv.Add(pageContent);

                return RedirectToAction("PageContents");
            }
            else
            {
                _log.Error("[CreatePageContent] ModelState not valid...");
                return View("CreatePageContent");
            }
        }

        public ViewResult Edit(int id)
        {
            PageContent pageContent = _pageContentSrv.GetPageContent(id);
            PageContentModel model = _mapper.Map<PageContentModel>(pageContent);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(PageContentModel model)
        {
            if (ModelState.IsValid)
            {
                PageContent pageContent = _mapper.Map<PageContent>(model);
                _pageContentSrv.UpdatePageContent(pageContent);
            }
            return RedirectToAction("PageContents");
        }
    }
}