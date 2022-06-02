﻿﻿using AutoMapper;
using Catalyte.Apparel.API.DTOMappings;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalyte.Apparel.DTOs.Purchases;
using Catalyte.Apparel.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Catalyte.Apparel.Data.Models;

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
        public async Task<ActionResult<PurchaseResponseDTO>> GetAllPurchasesAsync(string email)
        {
            _logger.LogInformation("Request received for GetAllPurchasesAsync");

            try
            {
                var purchaseByEmail = await _purchaseProvider.GetAllPurchasesAsync(email);
                var purchaseByEmailDTO = _mapper.MapPurchaseToPurchaseResponseDTO(purchaseByEmail);

                return Ok(purchaseByEmailDTO);
            }
            catch
            {
                return Ok(new string[] { });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Handle404()
        {
            return StatusCode(404);
        }

        [HttpPost]
        public async Task<ActionResult<List<PurchaseResponseDTO>>> CreatePurchaseAsync([FromBody] PurchaseRequestDTO model)
        {
            _logger.LogInformation("Request received for CreatePurchase");

            var newPurchase = _mapper.MapCreatePurchaseDTOToPurchase(model);
            var savedPurchase = await _purchaseProvider.CreatePurchaseAsync(newPurchase);
            var purchaseResponseDTO = _mapper.MapPurchaseToPurchaseResponseDTO(savedPurchase);

            if (purchaseResponseDTO != null)
            {
                return NoContent();
            }

            return Created($"/purchases/", purchaseResponseDTO);
        }
    }
}
