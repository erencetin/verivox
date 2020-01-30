using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verivox.TariffComparison.Core.Models;
using Verivox.TariffComparison.Core.Services;

namespace Verivox.TariffComparison.Test
{
    [TestFixture]
    public class RetrieveProductServiceTest
    {
        IRetrieveProductService _retrieveProductService;
        Dictionary<string, Func<decimal, decimal>> _expectedCalculation;
        [SetUp]
        public void Init()
        {
            var retrieveCalculationsMock = new Mock<IRetrieveCalculationService>();
            _expectedCalculation = new Dictionary<string, Func<decimal, decimal>>();
            _expectedCalculation.Add("Basic", null);
            _expectedCalculation.Add("Package", null);
            retrieveCalculationsMock.Setup(x => x.GetCalculations()).Returns(_expectedCalculation);
            _retrieveProductService = new RetrieveProductService(retrieveCalculationsMock.Object);
        }
        [Test]
        public void GetProductsShouldReturnProperProductList()
        {
            var expectedProductList = new List<Product>();
            expectedProductList.Add(new Product { Name = "Basic electricity tariff" });
            expectedProductList.Add(new Product { Name = "Packaged tariff" });
            var actualProductList = _retrieveProductService.GetProducts();
            for (int i = 0; i < expectedProductList.Count; i++)
            {
                Assert.That(actualProductList[i].Name, Is.EqualTo(expectedProductList[i].Name));
                Assert.That(actualProductList[i].CalculationModel, Is.EqualTo(expectedProductList[i].CalculationModel));

            }

        }
    }
}
