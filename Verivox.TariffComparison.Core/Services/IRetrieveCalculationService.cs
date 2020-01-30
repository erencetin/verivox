using System;
using System.Collections.Generic;

namespace Verivox.TariffComparison.Core.Services
{
    public interface IRetrieveCalculationService
    {
        Dictionary<string, Func<decimal, decimal>> GetCalculations();
    }
}