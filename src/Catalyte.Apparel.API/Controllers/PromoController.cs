using AutoMapper;
using Catalyte.Apparel.API.DTOMappings;
using Catalyte.Apparel.Providers.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Catalyte.Apparel.DTOs.Promos;
using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.API.Validators;

namespace Catalyte.Apparel.API.Controllers
{
    /// <summary>
    /// The PromoController exposes endpoints for promo code related actions.
    /// </summary>
    [ApiController]
    [Route("/promos")]
    public class PromoController : ControllerBase
    {
        private readonly ILogger<PromoController> _logger;
        private readonly IPromoProvider _promoProvider;
        private readonly IMapper _mapper;

        public PromoController(
            ILogger<PromoController> logger,
            IPromoProvider promoProvider,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _promoProvider = promoProvider;
        }

        /// <summary>
        /// Returns all promo codes currently in the database.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoDTO>>> GetPromosAsync()
        {
            _logger.LogInformation("Request received for GetPromosAsync");

            var promos = await _promoProvider.GetPromosAsync();
            var promoDTOs = _mapper.Map<IEnumerable<PromoDTO>>(promos);

            return Ok(promoDTOs);
        }

        /// <summary>
        /// Returns a single promo code given its id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PromoDTO>> GetPromoByIdAsync(int id)
        {
            _logger.LogInformation($"Request received for GetPromoByIdAsync for id: {id}");

            var promo = await _promoProvider.GetPromoByIdAsync(id);
            var promoDTO = _mapper.Map<PromoDTO>(promo);

            return Ok(promoDTO);
        }

        /// <summary>
        /// Stores a provided promo code to the database given user-defined fields.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PromoDTO>> CreatePromoAsync([FromBody] Promo promoToCreate)
        {

            _logger.LogInformation("Request received for CreatePromoAsync");

            // Validation occurs here
            promoToCreate.ValidatePromo();

            var promo = await _promoProvider.CreatePromoAsync(promoToCreate);
            var promoDTO = _mapper.Map<PromoDTO>(promo);

            return Created("/promos", promoDTO);
        }
    }
}
