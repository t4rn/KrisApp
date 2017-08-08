using AutoMapper;
using KrisApp.Controllers;
using KrisApp.DataModel.Articles;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Interfaces;
using KrisApp.Models.Articles;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KrisApp.Tests.Controllers
{
    [TestFixture]
    class ArticleControllerTest
    {
        private ArticleController _articleController;
        private Mock<IMapper> _mapperMock;
        private Mock<IDictionaryService> _dictionarySrvMock;
        private Mock<IArticleService> _articleSrvMock;

        [SetUp]
        protected void Setup()
        {
            Mock<ILogger> mockLogger = new Mock<ILogger>();
            _articleSrvMock = new Mock<IArticleService>();
            Mock<ISessionService> mockSession = new Mock<ISessionService>();
            _dictionarySrvMock = new Mock<IDictionaryService>();
            _mapperMock = new Mock<IMapper>();

            _articleController = new ArticleController(mockLogger.Object, _articleSrvMock.Object,
                _dictionarySrvMock.Object, _mapperMock.Object, mockSession.Object);
        }

        [TestCase(null, null)]
        [TestCase(null, "mvc")]
        public void ListArticlesAjaxRequest(string titlePart, string type)
        {
            // Arrange
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new WebHeaderCollection { { "X-Requested-With", "XMLHttpRequest" } });
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            _articleController.ControllerContext = new ControllerContext(context.Object, new RouteData(), _articleController);

            // Act
            ActionResult result = _articleController.List(titlePart, type);

            // Assert
            Assert.IsInstanceOf<PartialViewResult>(result);
            var model = (result as PartialViewResult).Model;
            Assert.IsInstanceOf<ArticleListModel>(model);
            Assert.AreEqual(type, (model as ArticleListModel).ArticleType);
        }

        [TestCase(null, null)]
        [TestCase("solid", "wcf")]
        public void ListArticlesNormalRequest(string titlePart, string type)
        {
            // Arrange
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(new WebHeaderCollection { });
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            _articleController.ControllerContext = new ControllerContext(context.Object, new RouteData(), _articleController);

            // Act
            ActionResult result = _articleController.List(titlePart, type);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var model = (result as ViewResult).Model;
            Assert.IsInstanceOf<ArticleListModel>(model);
            Assert.AreEqual(type, (model as ArticleListModel).ArticleType);
        }

        [Test]
        public void CreateArticleGetView()
        {
            // Dictionary mock
            var allArticleTypes = new List<ArticleType> {
                new ArticleType { Code = "ASP" },
                new ArticleType { Code = "WCF" },
                new ArticleType { Code = "Main", IsMain = true }
            };

            _dictionarySrvMock.Setup(m => m.GetDictionary<ArticleType>()).Returns(allArticleTypes);

            // AutoMapper mock
            var expectedMappedArticleTypes = new List<SelectListItem>() {
                new SelectListItem() { Text = "ASP" },
                new SelectListItem() { Text = "WCF" }
            };

            _mapperMock
                .Setup(m =>
                    m.Map<IEnumerable<SelectListItem>>
                    (It.IsAny<IEnumerable<ArticleType>>()))
                .Returns(expectedMappedArticleTypes);

            ViewResult actionResult = _articleController.CreateArticle();

            // type of model in ViewResult
            Assert.IsNotNull(actionResult);

            object model = actionResult.Model;
            Assert.IsInstanceOf<ArticleModel>(model);

            var properArticleTypes = _dictionarySrvMock.Object.GetDictionary<ArticleType>()
                .Where(x => x.IsMain == false);

            // was mock invoked
            _mapperMock.Verify(x => x.Map<IEnumerable<SelectListItem>>(properArticleTypes), Times.Once);

            // article types count
            Assert.AreEqual(properArticleTypes.Count(), ((ArticleModel)model).ArticleTypes.Count(),
                "Incorrect article types count");
        }

        [Test]
        public void CreateArticlePositive()
        {
            // Arrange
            var model = new ArticleModel() { Content = "Test Content" };
            var expectedMappedArticle = new Article() { Content = model.Content };

            _mapperMock
                .Setup(m =>
                    m.Map<Article>
                    (It.IsAny<ArticleModel>()))
                .Returns(expectedMappedArticle);

            var expectedArticle = new Article() { Id = 10 };
            _articleSrvMock
                .Setup(x => x.AddArticle(expectedMappedArticle))
                .Returns(expectedArticle);

            // Act
            ActionResult result = _articleController.CreateArticle(model);

            // Assert
            _mapperMock.Verify(x => x.Map<Article>(model), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToRouteResult>(result);
            Assert.IsNotNull(_articleController.TempData["Msg"]);
            Assert.AreEqual($"Artykuł dodany pomyślnie! Otrzymał ID = {expectedArticle.Id}.",
                _articleController.TempData["Msg"]);
        }

        [Test]
        public void CreateArticleInvalidModelState()
        {
            // Arrange
            _articleController.ModelState.AddModelError("fakeError", "Error for test");

            var allArticleTypes = new List<ArticleType> {
                new ArticleType { Code = "CODE1" }, new ArticleType { Code = "CODE2" } };
            _dictionarySrvMock.Setup(m => m.GetDictionary<ArticleType>()).Returns(allArticleTypes);

            var expectedMappedArticleTypes = new List<SelectListItem>() {
                new SelectListItem() { Text = "CODE1" }, new SelectListItem() { Text = "CODE2" } };
            _mapperMock
                .Setup(m =>
                    m.Map<IEnumerable<SelectListItem>>
                    (It.IsAny<IEnumerable<ArticleType>>()))
                .Returns(expectedMappedArticleTypes);

            // Act
            ActionResult result = _articleController.CreateArticle(new ArticleModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            var model = ((ViewResult)result).Model;
            Assert.IsInstanceOf<ArticleModel>(model);
            Assert.AreEqual(2, (model as ArticleModel).ArticleTypes.Count());
        }
    }
}
