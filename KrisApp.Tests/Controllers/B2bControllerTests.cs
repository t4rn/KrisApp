using KrisApp.Controllers.Api;
using KrisApp.Models.Calc;
using NUnit.Framework;
using System;
using System.Web.Http.Results;

namespace KrisApp.Tests.Controllers
{
    [TestFixture]
    public class B2bControllerTests
    {
        private B2bController _controller;

        [SetUp]
        protected void Setup()
        {
            _controller = new B2bController();
        }

        [Test]
        public void Get()
        {
            var result = _controller.Get() as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(result);
            Assert.AreEqual("OK from API", result.Content, 
                $"Incorrect result = '{result.Content}'");
        }

        [TestCase(10000, 175.92, 297.28, ExpectedResult = 7716.71)]
        [TestCase(12000, 175.92, 297.28, ExpectedResult = 9336.71)]
        [TestCase(10000, 749.94, 297.28, ExpectedResult = 7251.75)]
        [TestCase(12000, 749.94, 297.28, ExpectedResult = 8871.75)]
        public decimal Post(decimal netto, decimal spol, decimal zdro)
        {
            B2bAmountModel model = new B2bAmountModel()
            {
                NettoAmount = netto,
                SpolAmount = spol,
                ZdroAmount = zdro
            };

            var result = _controller.Post(model) as OkNegotiatedContentResult<decimal>;

            Assert.IsNotNull(result);

            return Math.Round(result.Content, 2);
        }
    }
}
