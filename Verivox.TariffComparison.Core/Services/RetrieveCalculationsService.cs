using System;
using System.Collections.Generic;
using System.Text;

namespace Verivox.TariffComparison.Core.Services
{
    public class RetrieveCalculationsService : IRetrieveCalculationService
    {
        private const decimal BaseCostPerMonth = 5;
        private const decimal ConsumptionUnitCost = 0.22m;
        private const decimal PackageMaxLimitConsumption = 4000;
        private const decimal PackageMaxLimitCost = 800;
        private const decimal PackageConsumptionUnitCost = 0.30m;
        public Dictionary<string,Func<decimal,decimal>> GetCalculations()
        {
            //These calculation models might be stored on db, in order to reduce deployment cost. 
            //To keep the scope simpler i am populating them below.
            Func<decimal, decimal> modelA = (consumption) => BaseCostPerMonth * 12 + consumption * ConsumptionUnitCost;
            Func<decimal, decimal> modelB = (consumption) =>
            {
                if (consumption <= PackageMaxLimitConsumption)
                {
                    return PackageMaxLimitCost;
                }
                else
                {
                    return (consumption - PackageMaxLimitConsumption) * PackageConsumptionUnitCost + PackageMaxLimitCost; 
                }
            };
            var calculationDict = new Dictionary<string, Func<decimal, decimal>>();
            calculationDict.Add("Basic", modelA);
            calculationDict.Add("Package", modelB);
            return calculationDict;
        }
    }
}
