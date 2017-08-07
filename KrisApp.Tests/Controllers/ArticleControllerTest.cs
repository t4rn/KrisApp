using AutoMapper;
using KrisApp.Controllers;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Interfaces;
using KrisApp.Models.Articles;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace KrisApp.Tests.Controllers
{
    [TestFixture]
    class ArticleControllerTest
    {
        private ArticleController _articleController;
        private Mock<IMapper> _mockMapper;
        private Mock<IDictionaryService> _mockDictionarySrv;

        [SetUp]
        protected void Setup()
        {
            Mock<ILogger> mockLogger = new Mock<ILogger>();
            Mock<IArticleService> mockArticleSrv = new Mock<IArticleService>();
            Mock<ISessionService> mockSession = new Mock<ISessionService>();
            _mockDictionarySrv = new Mock<IDictionaryService>();
            _mockMapper = new Mock<IMapper>();

            _articleController = new ArticleController(mockLogger.Object, mockArticleSrv.Object,
                _mockDictionarySrv.Object, _mockMapper.Object, mockSession.Object);
        }

        [Test]
        public void CreateArticle()
        {
            // Dictionary mock
            var allArticleTypes = new List<ArticleType> {
                new ArticleType { Code = "ASP" },
                new ArticleType { Code = "WCF" },
                new ArticleType { Code = "Main", IsMain = true }
            };

            _mockDictionarySrv.Setup(m => m.GetDictionary<ArticleType>()).Returns(allArticleTypes);

            // AutoMapper mock
            var expectedMappedArticleTypes = new List<SelectListItem>() {
                new SelectListItem() { Text = "ASP" },
                new SelectListItem() { Text = "WCF" }
            };

            _mockMapper
                .Setup(m =>
                    m.Map<IEnumerable<SelectListItem>>
                    (It.IsAny<IEnumerable<ArticleType>>()))
                .Returns(expectedMappedArticleTypes);

            ViewResult actionResult = _articleController.CreateArticle();

            // type of model in ViewResult
            Assert.IsNotNull(actionResult);

            object model = actionResult.Model;
            Assert.IsInstanceOf(typeof(ArticleModel), model);

            var properArticleTypes = _mockDictionarySrv.Object.GetDictionary<ArticleType>()
                .Where(x => x.IsMain == false);

            // was mock invoked
            _mockMapper.Verify(x => x.Map<IEnumerable<SelectListItem>>(properArticleTypes), Times.Once);

            // article types count
            Assert.AreEqual(properArticleTypes.Count(), ((ArticleModel)model).ArticleTypes.Count(),
                "Incorrect article types count");
        }
    }
}
