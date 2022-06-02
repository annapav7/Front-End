using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Providers.Providers;
using Catalyte.Apparel.DTOs.Filters;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Catalyte.Apparel.Test.Unit
{
    public class ProductUnitTests
    {
        private readonly ProductFilterQuery filterQuery;
        private readonly IProductProvider productProvider;
        private readonly Mock<IProductRepository> productRepo;
        private readonly Mock<ILogger<ProductProvider>> logger;

        public ProductUnitTests()
        {
            productRepo = new Mock<IProductRepository>();

            logger = new Mock<ILogger<ProductProvider>>();

            productProvider = new ProductProvider(productRepo.Object, logger.Object);

            filterQuery = new ProductFilterQuery();
        }

        [Fact]
        public async void GetProductsById_DatabaseException_Returns503()
        {
            productRepo.Setup(r => r.GetProductByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("test message"));

            await Assert.ThrowsAsync<ServiceUnavailableException>(() => productProvider.GetProductByIdAsync(1));
        }
        
        [Fact]
        public async void GetProductsById_NotFound_Returns404()
        {
            productRepo.Setup(r => r.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync((int productId) =>
            {
                return null;
            });

            await Assert.ThrowsAsync<NotFoundException>(() => productProvider.GetProductByIdAsync(1));

            productRepo.Setup(r => r.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync((int productId) =>
            {
                return default;
            });

            await Assert.ThrowsAsync<NotFoundException>(() => productProvider.GetProductByIdAsync(1));
        }
        
        [Fact]
        public async void GetProductsById_ValidId_ReturnsProduct()
        {
            productRepo.Setup(r => r.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync((int productId) =>
            {
                return new Product();
            });

            var actual = await productProvider.GetProductByIdAsync(1);

            Assert.NotNull(actual);
            Assert.IsType<Product>(actual);
        }
        
        [Fact]
        public async void GetProducts_DatabaseException_Returns503()
        {
            productRepo.Setup(r => r.GetProductsAsync(It.IsAny<ProductFilterQuery>())).ThrowsAsync(new Exception("test message"));

            await Assert.ThrowsAsync<ServiceUnavailableException>(() => productProvider.GetProductsAsync(filterQuery));
        }
        
        [Fact]
        public async void GetProducts_NullFilterQuery_ReturnsProducts()
        {
            productRepo.Setup(r => r.GetProductsAsync(It.IsAny<ProductFilterQuery>())).ReturnsAsync((ProductFilterQuery filterQuery) =>
            {
                Assert.NotNull(filterQuery);
                return Array.Empty<Product>();
            });

            var actual = await productProvider.GetProductsAsync(null);

            Assert.NotNull(actual);
            Assert.IsType<Product[]>(actual);
        }
        
        [Fact]
        public async void GetProducts_ValidFilterQuery_ReturnsProducts()
        {
            productRepo.Setup(r => r.GetProductsAsync(It.IsAny<ProductFilterQuery>())).ReturnsAsync((ProductFilterQuery filterQuery) =>
            {
                Assert.NotNull(filterQuery);
                return Array.Empty<Product>();
            });

            var filterQuery = new ProductFilterQuery()
            {
                Brand = new string[] { "Nike", "Adidas" },
                Category = new string[] { "Soccer", "Golf" },
                Demographic = new string[] { "Women", "Men" },
                PriceMin = 1m,
                PriceMax = 100m,
                Color = new string[] { "#ffffff", "#000000"},
                Material = new string[] { "Cotton", "Polyester" }
            };

            var actual = await productProvider.GetProductsAsync(filterQuery);

            Assert.NotNull(actual);
            Assert.IsType<Product[]>(actual);
        }

    }
}
