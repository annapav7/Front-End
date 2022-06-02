using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.DTOs.Filters;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Providers
{
    /// <summary>
    /// This class provides the implementation of the IProductProvider interface, providing service methods for products.
    /// </summary>
    public class ProductProvider : IProductProvider
    {
        private readonly ILogger<ProductProvider> _logger;
        private readonly IProductRepository _productRepository;

        public ProductProvider(IProductRepository productRepository, ILogger<ProductProvider> logger)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Asynchronously retrieves the product with the provided id from the database.
        /// </summary>
        /// <param name="productId">The id of the product to retrieve.</param>
        /// <returns>The product.</returns>
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            Product product;

            try
            {
                product = await _productRepository.GetProductByIdAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (product == null || product == default)
            {
                _logger.LogInformation($"Product with id: {productId} could not be found.");
                throw new NotFoundException($"Product with id: {productId} could not be found.");
            }

            return product;
        }

        /// <summary>
        /// Asynchronously retrieves all products from the database.
        /// </summary>
        /// <param name="filterQuery">Nullable. A ProductFilterQuery object containing the relevant queriable columns. Properties of the object that are null are ignored. The argument itself can be set to null and all products will be returned from the database unfiltered.</param>
        /// <returns>All products in the database.</returns>
        public async Task<IEnumerable<Product>> GetProductsAsync(ProductFilterQuery filterQuery)
        {
            IEnumerable<Product> products;

            if (filterQuery == null)
            {
                filterQuery = new ProductFilterQuery();
            }

            try
            {
                products = await _productRepository.GetProductsAsync(filterQuery);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return products;
        }


        
        /// <summary>
        /// Asyncronously retreives distinct categories from Database
        /// </summary>
        /// <returns> collection of strings of distinct categories in database</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        public async Task<IEnumerable<string>> GetDistinctCategoriesAsync()
        {
            IEnumerable<string> categories;

            try
            {
                categories = await _productRepository.GetDistinctCategoriesAsync(); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return categories;
        }

        /// <summary>
        /// Asyncronously retreives distinct types from Database
        /// </summary>
        /// <returns>collection of strings of distinct types in database</returns>
        /// <exception cref="ServiceUnavailableException"></exception>
        public async Task<IEnumerable<string>> GetDistinctTypesAsync()
        {
            IEnumerable<string> types;

            try
            {
                types = await _productRepository.GetDistinctTypesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return types;
        }
    }
}
