using System;
using System.Collections.Generic;
using System.Text;
using Verivox.TariffComparison.Core.Models;
using System.Linq;

namespace Verivox.TariffComparison.Core.Services
{
    public class CompareProductsService : ICompareProductsService
    {
        private readonly IRetrieveProductService _retrieveProductService;
        public CompareProductsService(IRetrieveProductService retrieveProductService)
        {
            _retrieveProductService = retrieveProductService;
        }
        public List<ComparisonResult> GetComparions(decimal consumption)
        {
            var resultList = new List<ComparisonResult>();
            var products = _retrieveProductService.GetProducts();
            foreach (var product in products)
            {
                var annualCost = product.CalculationModel(consumption);
                resultList.Add(new ComparisonResult { AnnualCosts = annualCost, TariffName = product.Name  });
            }
            return resultList.OrderBy(c => c.AnnualCosts).ToList();
        }
    }
}
