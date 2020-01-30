using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Verivox.TariffComparison.Core.Models;
using Verivox.TariffComparison.Core.Services;

namespace Verivox.TariffComparison.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompareProductsController : ControllerBase
    {
        ICompareProductsService _compareProductsService;
        public CompareProductsController(ICompareProductsService compareProductsService)
        {
            _compareProductsService = compareProductsService;
        }

        [HttpGet]
        public List<ComparisonResult> Get(decimal consumption)
        {
            return _compareProductsService.GetComparions(consumption);
        }
    }
}
