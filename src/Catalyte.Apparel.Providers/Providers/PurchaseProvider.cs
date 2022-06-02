using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Providers
{
    /// <summary>
    /// This class provides the implementation of the IPurchaseProvider interface, providing service methods for purchases.
    /// </summary>
    public class PurchaseProvider : IPurchaseProvider
    {
        private readonly ILogger<PurchaseProvider> _logger;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRespository;

        public PurchaseProvider(IPurchaseRepository purchaseRepository, ILogger<PurchaseProvider> logger, IProductRepository productRespository)
        {
            _logger = logger;
            _purchaseRepository = purchaseRepository;
            _productRespository = productRespository;
        }

        /// <summary>
        /// Retrieves all purchases from the database.
        /// </summary>
        /// <returns>All purchases.</returns>
        public async Task<Purchase> GetAllPurchasesAsync(string email)
        {
            Purchase purchases;

            try
            {
                purchases = await _purchaseRepository.GetAllPurchasesAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (purchases == default)
            {
                _logger.LogError($"Could not find user with email: {email}");
                throw new NotFoundException($"Could not find user with email: {email}");
            }

            return purchases;
        }

        /// <summary>
        /// Persists a purchase to the database.
        /// </summary>
        /// <param name="newPurchase">Purchase model used to build the purchase.</param>
        /// <returns>The persisted purchase with IDs.</returns>
        public async Task<Purchase> CreatePurchaseAsync(Purchase newPurchase)
        {
            Purchase savedPurchase;
            await InactiveProductException(newPurchase);

            try
            {
                savedPurchase = await _purchaseRepository.CreatePurchaseAsync(newPurchase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return savedPurchase;
        }

        /// <summary>
        /// Before a purchase persists to the database, this checks to see if there are any inactive products. If so, it throws an error.
        /// </summary>
        /// <param name="newPurchase">Purchase model used to build the purchase.</param>
        /// <returns>the new purchase if no errors occur</returns>
        /// <exception cref="UnprocessableEntityException">the error and message thrown when an inactive product is in the purchase</exception>
        public async Task<Purchase> InactiveProductException(Purchase newPurchase)
        {
            var items = newPurchase.LineItems;
            var inactiveProducts = new List<string>();

            Product product = default(Product);

            foreach (var item in items)
            {
                try
                {
                    product = await _productRespository.GetProductByIdAsync(item.ProductId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ServiceUnavailableException("There was a problem connecting to the database.");
                }

                if (product.Active == false)
                {
                    inactiveProducts.Add(product.Name);

                }
            }

            if (inactiveProducts.Count > 1)
            {
                string combinedProducts = string.Join(", ", inactiveProducts);
                throw new UnprocessableEntityException($"The following products are inactive and therefore cannot be purchased: {combinedProducts}.");
            }
            else if (inactiveProducts.Count == 1)
            {
                throw new UnprocessableEntityException($"The following product is inactive and therefore cannot be purchased: {product.Name}.");
            }
            else 
            {
                return newPurchase;
            }

        }

    }

}
