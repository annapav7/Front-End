using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Providers.Providers;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Catalyte.Apparel.Test.Unit
{
    public class PurchaseUnitTests
    {
        private readonly List<Purchase> purchases;
        private readonly IPurchaseProvider purchaseProvider;
        private readonly Mock<IPurchaseRepository> purchaseRepo;
        private readonly Mock<IProductRepository> productRepo;
        private readonly Mock<ILogger<PurchaseProvider>> logger;

        public PurchaseUnitTests()
        {
            purchaseRepo = new Mock<IPurchaseRepository>();

            logger = new Mock<ILogger<PurchaseProvider>>();

            productRepo = new Mock<IProductRepository>();

            purchaseProvider = new PurchaseProvider(purchaseRepo.Object, logger.Object, productRepo.Object);

            purchases = new List<Purchase>()
            {
                new Purchase()
                {
                    Id = 1,
                    BillingCity = "Atlanta",
                    BillingEmail = "customer@home.com",
                    BillingPhone = "(714) 345-8765",
                    BillingState = "GA",
                    BillingStreet = "123 Main",
                    BillingStreet2 = "Apt A",
                    BillingZip = "31675",
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    DeliveryCity = "Birmingham",
                    DeliveryState = "AL",
                    DeliveryStreet = "123 Hickley",
                    DeliveryZip = 43690,
                    DeliveryFirstName = "Max",
                    DeliveryLastName = "Space",
                    CardHolder = "Max Perkins",
                    CardNumber = "1435678998761234",
                    Expiration = "11/21",
                    CVV = 456,
                    OrderDate = new DateTime(2021, 5, 4)
                }
            };
        }

        [Fact]
        public async void GetPurchases_DatabaseException_Returns503()
        {
            purchaseRepo.Setup(r => r.GetAllPurchasesAsync("/")).ThrowsAsync(new Exception("test message"));

            await Assert.ThrowsAsync<ServiceUnavailableException>(() => purchaseProvider.GetAllPurchasesAsync("/"));
        }

        [Fact]
        public async void PostPurchase_ValidPurchase_ReturnsPurchase()
        {
            purchaseRepo.Setup(r => r.CreatePurchaseAsync(It.IsAny<Purchase>())).ReturnsAsync((Purchase target) =>
            {
                // ensures id is set to be the next one in list
                target.Id = purchases.Count + 1;

                // adds current purchase to list
                purchases.Add(target);

                // return updated purchase
                return target;
            });

            var product = new Product()
            {
                Name = "Shoes",
                Id = 4,
                Active = true,
            };

            //mock product repository
            productRepo.Setup(x => x.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            var purchase = new Purchase()
            {
                BillingCity = "Atlanta",
                BillingEmail = "test@test.com",
                BillingPhone = "(714) 345-8765",
                BillingState = "GA",
                BillingStreet = "123 Main",
                BillingStreet2 = "Apt A",
                BillingZip = "31675",
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                DeliveryCity = "Birmingham",
                DeliveryState = "AL",
                DeliveryStreet = "123 Farewell st",
                DeliveryZip = 33716,
                DeliveryFirstName = "George",
                DeliveryLastName = "Sparks",
                CardHolder = "George Sparks",
                CardNumber = "1856972658932587",
                Expiration = "20/25",
                CVV = 759,
                OrderDate = new DateTime(2021, 5, 4),
                LineItems = new List<LineItem>()
                {
                    new LineItem()
                    {
                        ProductId = 4,
                        Quantity = 1,
                    }
                }
            };

            var actual = await purchaseProvider.CreatePurchaseAsync(purchase);

            Assert.NotNull(actual);
            Assert.IsType<Purchase>(actual);
            Assert.Equal(purchases.Count, actual.Id);
        }
    }
}

