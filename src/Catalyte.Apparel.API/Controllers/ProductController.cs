﻿using AutoMapper;
﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Catalyte.Apparel.DTOs.Products;
using Catalyte.Apparel.DTOs.Filters;
using Catalyte.Apparel.Providers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalyte.Apparel.API.Controllers
{
    /// <summary>
    /// The ProductsController exposes endpoints for product related actions.
    /// </summary>
    [ApiController]
    [Route("/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductProvider _productProvider;
        private readonly IMapper _mapper;

        public ProductController(
            ILogger<ProductController> logger,
            IProductProvider productProvider,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _productProvider = productProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsAsync([FromQuery] ProductFilterQuery filterQuery)
        {
            _logger.LogInformation("Request received for GetProductsAsync");

            var products = await _productProvider.GetProductsAsync(filterQuery);
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)

        {
            _logger.LogInformation($"Request received for GetProductByIdAsync for id: {id}");

            var product = await _productProvider.GetProductByIdAsync(id);
            var productDTO = _mapper.Map<ProductDTO>(product);

            return Ok(productDTO);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetDistinctCategoriesAsync()
        {
            _logger.LogInformation("Request received for GetProductsAsync");

            var categories = await _productProvider.GetDistinctCategoriesAsync();
            var productDTOs = _mapper.Map<IEnumerable<string>>(categories);

            return Ok(productDTOs);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<string>>> GetDistinctTypesAsync()
        {
            _logger.LogInformation("Request received for GetDistinctTypesAsync");

            var types = await _productProvider.GetDistinctTypesAsync();
            var productDTOs = _mapper.Map<IEnumerable<string>>(types);

            return Ok(productDTOs);
        }
    }
}
