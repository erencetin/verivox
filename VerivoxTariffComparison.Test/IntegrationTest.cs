using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Verivox.TariffComparison.Core.Models;

namespace Verivox.TariffComparison.Test
{
    [TestFixture]
    public class IntegrationTest
    {
        private WebApiTesterFactory _factory;
        [SetUp]
        public void Init()
        {
            _factory = new WebApiTesterFactory();
        }
        [Test]
        public async Task ServiceShouldResponseSuccessStatusCode()
        {
            var httpClient = _factory.CreateClient();
            var response = await httpClient.GetAsync("compareproducts");
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
        [Test]
        public async Task CompareProductsMethodShouldResponseData()
        {
            var httpClient = _factory.CreateClient();
            var response = await httpClient.GetAsync("compareproducts?consumption=3500");
            var expectedCosts = new decimal[2] { 800, 830 }; 
            var content = await response.Content.ReadAsStringAsync();
            var comparisonList = JsonConvert.DeserializeObject<List<ComparisonResult>>(content);
            for (int i = 0; i < comparisonList.Count; i++)
            {
                Assert.IsTrue(expectedCosts[i] == comparisonList[i].AnnualCosts);
            }
        }

    }

    public class WebApiTesterFactory : WebApplicationFactory<Startup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            base.ConfigureWebHost(builder);
        }
    }
}
