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
    public class PurchaseInactiveProductsTests
    {
        private readonly List<Purchase> purchases;
        private readonly IPurchaseProvider purchaseProvider;
        private readonly Mock<IPurchaseRepository> purchaseRepo;
        private readonly Mock<IProductRepository> productRepo;
        private readonly Mock<ILogger<PurchaseProvider>> logger;
        private readonly List<Product> products;

        public PurchaseInactiveProductsTests()
        {
            purchaseRepo = new Mock<IPurchaseRepository>();

            productRepo = new Mock<IProductRepository>();

            logger = new Mock<ILogger<PurchaseProvider>>();

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

                    CVV = "456",

                    OrderDate = new DateTime(2021, 5, 4),
                    LineItems = new List<LineItem>()
                    {
                        new LineItem()
                        {
                            ProductId = 4,
                            Quantity = 1,
                        }
                    }
                }
            };

            products = new List<Product>()
            {
                  new Product
                  {
                      Name = "Shoes",
                      Id = 4,
                      Active = false,
                  },
                  new Product
                  {
                      Name = "Pants",
                      Id = 5,
                      Active = true,
                  },
                  new Product
                  {
                      Name = "Socks",
                      Id = 7,
                      Active = false,
                  }
            };
        }

        [Fact]
        public async void PostPurchase_OneInvalidProduct_ReturnsError()
        {
            //mock purchase repository
            purchaseRepo.Setup(r => r.CreatePurchaseAsync(It.IsAny<Purchase>())).ReturnsAsync((Purchase target) =>
            {
                // ensures id is set to be the next one in list
                target.Id = purchases.Count + 1;

                // adds current purchase to list
                purchases.Add(target);

                // return updated purchase
                return target;
            });

            //fake product to use as comparison
            var product = new Product()
            {
                Name = "Shoes",
                Id = 4,
                Active = false,
            };

            //mock product repository
            productRepo.Setup(x => x.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            //fake purchase to create
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

                CVV = "759",


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

            await Assert.ThrowsAsync<UnprocessableEntityException>(() => purchaseProvider.CreatePurchaseAsync(purchase));
        }

        [Fact]
        public async void PostPurchase_TwoInvalidProducts_ReturnsError()
        {
            //mock purchase repository
            purchaseRepo.Setup(r => r.CreatePurchaseAsync(It.IsAny<Purchase>())).ReturnsAsync((Purchase target) =>
            {
                // ensures id is set to be the next one in list
                target.Id = purchases.Count + 1;

                // adds current purchase to list
                purchases.Add(target);

                // return updated purchase
                return target;
            });

            //fake product to use as comparison
            var products = new List<Product>()
            {
                  new Product
                  {
                      Name = "Shoes",
                      Id = 4,
                      Active = false,
                  },
                  new Product
                  {
                      Name = "Socks",
                      Id = 7,
                      Active = false,
                  }
            };

            //mock product repository
            foreach (var product in products)
            {
                productRepo.Setup(x => x.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            }
            
            //fake purchase to create
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
                CVV = "759",

                OrderDate = new DateTime(2021, 5, 4),
                LineItems = new List<LineItem>()
                {
                    new LineItem()
                    {
                        ProductId = 4,
                        Quantity = 1,
                    },
                    new LineItem()
                    {
                        ProductId = 7,
                        Quantity = 1,
                    }
                }
            };

            await Assert.ThrowsAsync<UnprocessableEntityException>(() => purchaseProvider.CreatePurchaseAsync(purchase));
        }


        [Fact]
        public async void PostPurchase_OneInvalidAndOneValidProduct_ReturnsError()
        {
            //mock purchase repository
            purchaseRepo.Setup(r => r.CreatePurchaseAsync(It.IsAny<Purchase>())).ReturnsAsync((Purchase target) =>
            {
                // ensures id is set to be the next one in list
                target.Id = purchases.Count + 1;

                // adds current purchase to list
                purchases.Add(target);

                // return updated purchase
                return target;
            });

            //fake products to use as comparison
            var products = new List<Product>()
            {
                  new Product
                  {
                      Name = "Shoes",
                      Id = 4,
                      Active = true,
                  },
                  new Product
                  {
                      Name = "Pants",
                      Id = 5,
                      Active = false,
                  }
            };

            //mock product repository
            foreach (var product in products)
            {
                productRepo.Setup(x => x.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            }

            //fake purchase to create
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
                CVV = "759",
                OrderDate = new DateTime(2021, 5, 4),
                LineItems = new List<LineItem>()
                {
                    new LineItem()
                    {
                        ProductId = 4,
                        Quantity = 1,
                    },
                    new LineItem()
                    {
                        ProductId = 5,
                        Quantity = 1,
                    }
                }
            };

            await Assert.ThrowsAsync<UnprocessableEntityException>(() => purchaseProvider.CreatePurchaseAsync(purchase));
        }
    }
}