using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Verivox.TariffComparison.Core.Models;
using Verivox.TariffComparison.Core.Services;

namespace Verivox.TariffComparison.Test
{
    [TestFixture]
    public class CompareProductsServiceTest
    {
        ICompareProductsService _compareProductsService;
        [SetUp]
        public void Init()
        {
            var retrieveCalculationMethodsMock = new Mock<IRetrieveCalculationService>();
            retrieveCalculationMethodsMock.Setup(x => x.GetCalculations()).Returns(GenerateFakeCalculations());
            var retrieveProductsServiceMock = new Mock<RetrieveProductService>(retrieveCalculationMethodsMock.Object);
            
            _compareProductsService = new CompareProductsService(retrieveProductsServiceMock.Object);
        }
        private Dictionary<string, Func<decimal, decimal>> GenerateFakeCalculations()
        {
            var expectedCalculation = new Dictionary<string, Func<decimal, decimal>>();
            expectedCalculation.Add("Basic", (x)=>x);
            expectedCalculation.Add("Package", (x) =>x);
            return expectedCalculation;
        }
        [Test]
        public void GetComparionsShoudReturnProperComparisons()
        {
            decimal inputConsumption = 1;
            decimal expectedConsumption = 1;
            List<string> names = new List<string>() { "Basic electricity tariff", "Packaged tariff" };
            var comparisons = _compareProductsService.GetComparions(inputConsumption);
            foreach (var result in comparisons)
            {
                Assert.That(result.AnnualCosts, Is.EqualTo(expectedConsumption));
                Assert.IsTrue(names.Contains(result.TariffName));
            }

        }

    }
}
