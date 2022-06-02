using Catalyte.Apparel.DTOs.Products;
using Catalyte.Apparel.DTOs.Purchases;
using Catalyte.Apparel.Test.Integration.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Catalyte.Apparel.Test.Integration
{
    [Collection("Sequential")]
    public class PurchaseIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public PurchaseIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

         [Fact]
         public async Task GetPurchaseWithEmail_Returns200()
         {
             var response = await _client.GetAsync("/purchases/customer@home.com");
             Assert.Equal(HttpStatusCode.OK, response.StatusCode);
         }

        [Fact]
        public async Task GetPurchaseWithEmail_EmptyArray()
        {
            var response = await _client.GetAsync("/purchases/alrxkali@gmail.com");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
         public async Task GetPurchase_Returns404()
         {
             var response = await _client.GetAsync("/purchases");
             Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
         }
    }
}
