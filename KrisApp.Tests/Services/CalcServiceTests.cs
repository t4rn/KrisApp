using FluentAssertions;
using KrisApp.DataModel.Interfaces;
using KrisApp.Services;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace KrisApp.Tests.Services
{
    [TestFixture]
    public class CalcServiceTests
    {
        private CalcService _calcService;

        [SetUp]
        protected void Setup()
        {
            Mock<ILogger> mockLogger = new Mock<ILogger>();
            _calcService = new CalcService(mockLogger.Object);
        }

        [TestCase(1000, 42764, ExpectedResult = 10920)]
        [TestCase(5000, 42764, ExpectedResult = 54600)]
        [TestCase(10000, 42764, ExpectedResult = 106098)]
        [TestCase(15000, 42764, ExpectedResult = 155298)]
        public decimal CalculateUodAmount(decimal brutto, decimal limit)
        {
            // Arrange
            
            // Act
            var result = _calcService.CalculateIncome(brutto, limit);

            // Assert
            result.Should().NotBeNull();

            return result.Sum;
        }
    }
}
