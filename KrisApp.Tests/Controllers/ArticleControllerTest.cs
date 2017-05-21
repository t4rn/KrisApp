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
        [SetUp]
        protected void Setup()
        {
        }

        [Test]
        public void CreateArticle()
        {
            Mock<ILogger> mockLogger = new Mock<ILogger>();
            Mock<IArticleService> mockArticleSrv = new Mock<IArticleService>();
            Mock<IDictionaryService> mockDictionarySrv = new Mock<IDictionaryService>();
            mockDictionarySrv.Setup(m => m.GetDictionary<ArticleType>()).Returns(new List<ArticleType> {
                new ArticleType { Code = "ASP" },
                new ArticleType { Code = "WCF" },
            });

            Mock<IMapper> mockMapper = new Mock<IMapper>();
            Mock<ISessionService> mockSession = new Mock<ISessionService>();

            ArticleController articleController = new ArticleController(mockLogger.Object, mockArticleSrv.Object,
                mockDictionarySrv.Object, mockMapper.Object, mockSession.Object);

            ViewResult actionResult = articleController.CreateArticle();

            // typ modelu w zwracanym widoku
            object model = actionResult.Model;
            Assert.IsInstanceOf(typeof(ArticleModel), model);

            // liczba typów artykułów
            List<ArticleType> articleTypes = mockDictionarySrv.Object.GetDictionary<ArticleType>();
            Assert.IsTrue(((ArticleModel)model).ArticleTypes.Count() == articleTypes.Count , 
                "Niepoprawna liczba elementów");
        }
    }
}
