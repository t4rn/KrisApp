using AutoMapper;
using FluentAssertions;
using KrisApp.Controllers;
using KrisApp.DataModel.Calc;
using KrisApp.DataModel.Interfaces;
using KrisApp.Models.Calc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KrisApp.Tests.Controllers.Web
{
    [TestFixture]
    public class CalcControllerTests
    {
        private CalcController _controller;
        private Mock<ICalcService> _calcMock;
        private Mock<ISessionService> _sessionMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _calcMock = new Mock<ICalcService>();
            _sessionMock = new Mock<ISessionService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CalcController(_calcMock.Object, _sessionMock.Object, _mapperMock.Object);

            //HttpContext.Current = MockHelpers.FakeHttpContext();
        }

        [Test]
        public void B2bHttpGet()
        {
            // Arrange

            // Act
            ActionResult result = _controller.B2b();

            // Assert
            result.Should().NotBeNull().And.BeOfType<ViewResult>();
            ViewResult viewResult = result as ViewResult;
        }

        [Test]
        public void UodHttpGet()
        {
            // Arrange
            List<UodSummaryModel> expectedSummaries = new List<UodSummaryModel>()
            { new UodSummaryModel() { Average = "1000", Brutto = "15000" } };
            _sessionMock.Setup(x => x.GetFromSession<List<UodSummaryModel>>(SessionItem.Uod))
                .Returns(expectedSummaries);

            // Act
            ActionResult result = _controller.Uod();

            // Assert
            _sessionMock.Verify(x => x.GetFromSession<List<UodSummaryModel>>(SessionItem.Uod), Times.Once);

            result.Should().NotBeNull().And.BeOfType<ViewResult>();
            ViewResult viewResult = result as ViewResult;
            viewResult.Model.Should().NotBeNull().And.BeOfType<UodModel>();
            var model = viewResult.Model as UodModel;
            model.BruttoAmountPerMonth.Should().BeGreaterThan(0);
            model.Limit.Should().BeGreaterThan(0);
            model.SavedSummaries.Should().Equal(expectedSummaries);
        }

        [Test]
        public void UodHttpPost()
        {
            // Arrange
            UodModel inputModel = new UodModel() { BruttoAmountPerMonth = 10000, Limit = 42764 };
            UodSummary expectedSummary = new UodSummary()
            {
                Brutto = inputModel.BruttoAmountPerMonth,
                NettoAmounts = new Dictionary<string, decimal>()
                {
                    { "styczeń", 4000 }
                }
            };
            UodSummaryModel expectedSummaryModel = new UodSummaryModel() { Brutto = expectedSummary.Brutto.ToString() };
            List<UodSummaryModel> expectedSummaries = new List<UodSummaryModel>()
            { new UodSummaryModel() { Average = "1000", Brutto = "15000" } };

            _calcMock.Setup(x => x.CalculateIncome(inputModel.BruttoAmountPerMonth, inputModel.Limit))
                .Returns(expectedSummary);

            _mapperMock.Setup(x => x.Map<UodSummaryModel>(expectedSummary))
                .Returns(expectedSummaryModel);

            _sessionMock.Setup(x => x.GetFromSession<List<UodSummaryModel>>(SessionItem.Uod))
                .Returns(expectedSummaries);

            // Act
            ActionResult result = _controller.Uod(inputModel);

            // Assert
            _calcMock.Verify(x => x.CalculateIncome(inputModel.BruttoAmountPerMonth, inputModel.Limit), Times.Once);
            _sessionMock.Verify(x => x.GetFromSession<List<UodSummaryModel>>(SessionItem.Uod), Times.Once);
            _mapperMock.Verify(x => x.Map<UodSummaryModel>(expectedSummary), Times.Once);
            result.Should().NotBeNull().And.BeOfType<ViewResult>();
            ViewResult viewResult = result as ViewResult;
            viewResult.Model.Should().NotBeNull().And.BeOfType<UodModel>();
            var model = viewResult.Model as UodModel;
            model.BruttoAmountPerMonth.Should().BeGreaterThan(0);
            model.Limit.Should().BeGreaterThan(0);
            model.SavedSummaries.Should().Equal(expectedSummaries);
            model.CurrentSummary.Should().NotBeNull().And.Be(expectedSummaryModel);
        }
    }
}
