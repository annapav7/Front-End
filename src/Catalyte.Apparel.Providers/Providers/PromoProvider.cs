using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.Providers.Interfaces;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;

namespace Catalyte.Apparel.Providers.Providers
{
    /// <summary>
    /// This class provides the implementation of the IPromoProvider interface, providing service methods for promo codes.
    /// </summary>
    public class PromoProvider : IPromoProvider
    {
        private readonly ILogger<PromoProvider> _logger;
        private readonly IPromoRepository _promoRepository;

        public PromoProvider(IPromoRepository promoRepository, ILogger<PromoProvider> logger)
        {
            _logger = logger;
            _promoRepository = promoRepository;
        }

        /// <summary>
        /// Asynchronously retrieves the promo with the provided id from the database.
        /// </summary>
        /// <param name="promoId">The id of the promo to retrieve.</param>
        /// <returns>The promo.</returns>
        public async Task<Promo> GetPromoByIdAsync(int promoId)
        {
            Promo promo;

            try
            {
                promo = await _promoRepository.GetPromoByIdAsync(promoId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (promo == null || promo == default)
            {
                _logger.LogInformation($"Promo with id: {promoId} could not be found.");
                throw new NotFoundException($"Promo with id: {promoId} could not be found.");
            }

            return promo;
        }

        /// <summary>
        /// Asynchronously retrieves all promos from the database.
        /// </summary>
        /// <returns>All promos in the database.</returns>
        public async Task<IEnumerable<Promo>> GetPromosAsync()
        {
            IEnumerable<Promo> promos;

            try
            {
                promos = await _promoRepository.GetPromosAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return promos;
        }

        /// <summary>
        /// Persists a promo to the database given the provided title is not already in the database or null.
        /// </summary>
        /// <param name="newPromo">The promo to persist.</param>
        /// <returns>The promo.</returns>
        public async Task<Promo> CreatePromoAsync(Promo newPromo)
        {

            // CHECK TO MAKE SURE THE PROMO DOESN'T ALREADY EXIST
            Promo existingPromo;

            try
            {
                existingPromo = await _promoRepository.GetPromoByTitleAsync(newPromo.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            if (existingPromo != default)
            {
                _logger.LogError("Promo already exists.");
                throw new ConflictException("Promo already exists");
            }

            Promo savedPromo;

            try
            {
                savedPromo = await _promoRepository.CreatePromoAsync(newPromo);
                _logger.LogInformation("Promo saved.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ServiceUnavailableException("There was a problem connecting to the database.");
            }

            return savedPromo;
        }
    }
}
