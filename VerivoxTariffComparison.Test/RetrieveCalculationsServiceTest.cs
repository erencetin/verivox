using Moq;
using NUnit.Framework;
using System;
using Verivox.TariffComparison.Core.Services;

namespace Verivox.TariffComparison.Test
{
    [TestFixture]
    public class RetrieveCalculationsServiceTest
    {
        IRetrieveCalculationService _retrieveCalculationService;
        [SetUp]
        public void Init()
        {
            _retrieveCalculationService = new RetrieveCalculationsService();
        }
        [Test]
        public void GetCalculationShouldReturnCalculationMethodList()
        {
            var expectedCalculationMethodCount = 2;
            var actualItemCount = _retrieveCalculationService.GetCalculations().Count;
            Assert.That(actualItemCount, Is.EqualTo(expectedCalculationMethodCount));
        }
        [TestCase("Basic",3500,830)]
        [TestCase("Basic", 4500, 1050)]
        [TestCase("Basic", 6000, 1380)]
        [TestCase("Package", 3500, 800)]
        [TestCase("Package", 4500, 950)]
        [TestCase("Package", 6000, 1400)]
        public void CalculationModelsShouldReturnCorrectResulsts(string calculationModel, decimal consumption, decimal expectedResult)
        {
            var calculationModels = _retrieveCalculationService.GetCalculations();
            var actualResult = calculationModels[calculationModel](consumption);
            Assert.That(actualResult, Is.EqualTo(expectedResult));

        }
    }
}
