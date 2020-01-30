using System;
using System.Collections.Generic;
using System.Text;
using Verivox.TariffComparison.Core.Models;

namespace Verivox.TariffComparison.Core.Services
{
    public class RetrieveProductService : IRetrieveProductService
    {
        private readonly IRetrieveCalculationService _retrieveCalculationService;
        public RetrieveProductService(IRetrieveCalculationService retrieveCalculationService)
        {
            _retrieveCalculationService = retrieveCalculationService;
        }
        public List<Product> GetProducts()
        {
            List<Product> productList = new List<Product>();
            var productA = new Product();
            var calculations = _retrieveCalculationService.GetCalculations();
            productA.Name = "Basic electricity tariff";
            productA.CalculationModel = calculations["Basic"];
            productList.Add(productA);
            var productB = new Product();
            productB.Name = "Packaged tariff";
            productB.CalculationModel = calculations["Package"];
            productList.Add(productB);
            return productList;
        }
    }
}
