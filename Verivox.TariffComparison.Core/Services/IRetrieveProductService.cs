using System.Collections.Generic;
using Verivox.TariffComparison.Core.Models;

namespace Verivox.TariffComparison.Core.Services
{
    public interface IRetrieveProductService
    {
        List<Product> GetProducts();
    }
}