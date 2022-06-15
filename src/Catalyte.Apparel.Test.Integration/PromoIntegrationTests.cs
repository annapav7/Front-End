using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.DTOs.Promos;
using Catalyte.Apparel.Test.Integration.Utilities;
using Catalyte.Apparel.TestBase.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catalyte.Apparel.Test.Integration
{
    [Collection("Sequential")]
    public class PromoIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public PromoIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetPromos_Returns200()
        {
            var response = await _client.GetAsync("/promos");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetPromoById_GivenExistingId_Returns200()
        {
            var response = await _client.GetAsync("/promos/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsAsync<PromoDTO>();
            Assert.Equal(1, content.Id);
        }

        [Fact]
        public async Task CreatePromo_Returns201()
        {

            Promo testPromo = PromoHelper.GenerateValidFlatPromo();

            string json = JsonConvert.SerializeObject(testPromo);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/promos", httpContent);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
