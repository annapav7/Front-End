using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Catalyte.Apparel.API.DTOMappings;
using Catalyte.Apparel.DTOs.Purchases;
using Catalyte.Apparel.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalyte.Apparel.API.Controllers
{
    /// <summary>
    /// The PurchasessController exposes endpoints for purchase related actions.
    /// </summary>
    [ApiController]
    [Route("/purchases")]

    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<PurchaseController> _logger;
        private readonly IPurchaseProvider _purchaseProvider;
        private readonly IMapper _mapper;

        public PurchaseController(
            ILogger<PurchaseController> logger,
            IPurchaseProvider purchaseProvider,
            IMapper mapper
        )
        {
            _logger = logger;
            _purchaseProvider = purchaseProvider;
            _mapper = mapper;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<PurchaseResponseDTO>> GetAllPurchasesByEmailAsync(string email)
        {
            _logger.LogInformation("Request received for GetAllPurchasesAsync");

            var purchaseByEmail = await _purchaseProvider.GetAllPurchasesByEmailAsync(email);
            var purchaseByEmailDTO = _mapper.MapPurchasesToPurchaseResponseDTOs(purchaseByEmail);


            return Ok(purchaseByEmailDTO);

        }

        /// <summary>
        /// Endpoint for get all purchases with no email
        /// </summary>
        [HttpGet]
        public void GetAllPurchasesAsync()
        {
            _purchaseProvider.GetAllPurchasesAsync();
        }


        /// <summary>
        /// Helper set up to test the purchase response behavior when purchase request is made through valid information
        /// or invalid information. Purchase persists in repository when purchase request is made with all the valid information.
        /// When the request is not made with valid information, purchase doesn't persist in the repository.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<List<PurchaseResponseDTO>>> CreatePurchaseAsync([FromBody] PurchaseRequestDTO model)
        {
            _logger.LogInformation("Request received for CreatePurchase");

            var newPurchase = _mapper.MapCreatePurchaseDTOToPurchase(model);
            var savedPurchase = await _purchaseProvider.CreatePurchaseAsync(newPurchase);
            var purchaseResponseDTO = _mapper.MapPurchaseToPurchaseResponseDTO(savedPurchase);

            return Created($"/purchases/", purchaseResponseDTO);
        }

    }
}
