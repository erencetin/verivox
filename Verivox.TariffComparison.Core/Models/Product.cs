using System;
using System.Collections.Generic;
using System.Text;

namespace Verivox.TariffComparison.Core.Models
{
    public class Product
    {
        public string Name { get; set; }
        public Func<decimal,decimal> CalculationModel { get; set; }
    }
}
