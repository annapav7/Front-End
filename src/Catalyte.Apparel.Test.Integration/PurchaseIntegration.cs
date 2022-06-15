using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.DTOs.Purchases;
using Catalyte.Apparel.Test.Integration.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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
        public async Task CreatePurchaseAsync_Returns201()
        {
            Purchase purchase = new()
            {
                OrderDate = System.DateTime.Now,

                DeliveryFirstName = "Max",
                DeliveryLastName = "Space",
                DeliveryStreet = "123 Hickley",
                DeliveryStreet2 = null,
                DeliveryCity = "Birmingham",
                DeliveryState = "AL",
                DeliveryZip = 43690,

                BillingStreet = "123 Main",
                BillingStreet2 = "Apt A",
                BillingCity = "Atlanta",
                BillingState = "GA",
                BillingZip = "31675",
                BillingEmail = "customer@home.com",
                BillingPhone = "(714) 345-8765",

                CardNumber = "4435678998761234",
                CVV = "456",
                Expiration = "11/23",
                CardHolder = "Max Perkins",

            };

            string json = JsonConvert.SerializeObject(purchase);

            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/purchases", httpContent);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
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

        [Fact]
        public async Task PurchaseRequest_GivenInvalidInfo_ReturnsBadRequest()
        {
            var purchaseRequest = new PurchaseResponseDTO()
            {
                Id = 1,
                OrderDate = new DateTime(2021, 5, 4),
                DeliveryAddress = new DeliveryAddressDTO()
                {
                    DeliveryCity = "Birmingham",
                    DeliveryState = "AL",
                    DeliveryStreet = "123 Hickley",
                    DeliveryZip = 43690,
                    DeliveryFirstName = "Max",
                    DeliveryLastName = "Space",
                },
                BillingAddress = new BillingAddressDTO()
                {
                    BillingCity = "Atlanta",
                    Email = "customer@home.com",
                    Phone = "(714) 345-8765",
                    BillingState = "GA",
                    BillingStreet = "123 Main",
                    BillingStreet2 = "Apt A",
                    BillingZip = 31675,
                },
                CreditCard = new CreditCardDTO()
                {
                    CardHolder = "Max Perkins",
                    CardNumber = "1234567812345678",
                    Expiration = "12/25",
                    CVV = "",
                },
                LineItems = new List<LineItemDTO>()
                {
                    new LineItemDTO()
                    {
                    ProductId = 1,
                    Quantity = 1,
                }
                },
            };
            var response = await _client.PostAsync("/purchases",
                           new StringContent(JsonConvert.SerializeObject(purchaseRequest), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}
