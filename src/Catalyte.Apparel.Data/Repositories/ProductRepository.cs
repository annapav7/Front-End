using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Filters;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalyte.Apparel.DTOs.Filters;
using System.Linq;

namespace Catalyte.Apparel.Data.Repositories
{
    /// <summary>
    /// This class handles methods for making requests to the product repository.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly IApparelCtx _ctx;
        /// <summary>
        /// context assignment
        /// </summary>
        /// <param name="ctx"></param> 
        public ProductRepository(IApparelCtx ctx)
        {
            _ctx = ctx;
        }
        /// <summary>
        /// queries context DB and selects product by Product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _ctx.Products
                .AsNoTracking()
                .WhereProductIdEquals(productId)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Get all products from database with optional FilterQuery object.
        /// </summary>
        /// <param name="filterQuery">A ProductFilterQuery object containing the relevant queriable columns. Properties of the object that are null are ignored. The argument itself can be set to null and all products will be returned from the database unfiltered.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProductsAsync(ProductFilterQuery filterQuery)
        {

            return await _ctx.Products
                .AsNoTracking()
                .WhereProductMatchesFilterQuery(filterQuery)
                .OrderBy(products => products.Id)
                .ToListAsync();
        }
        /// <summary>
        /// queries context DB and selects distinct categories
        /// </summary>
        /// <returns>list of strings of distinct types</returns>
        public async Task<IEnumerable<string>> GetDistinctCategoriesAsync()
        {
            return await _ctx.Products
                .AsNoTracking()
                .SelectDistinctCategories()
                .ToListAsync();


        }
        /// <summary>
        /// queries context DB and selects distinct types
        /// </summary>
        /// <returns> list of strings of distinct types</returns>
        public async Task<IEnumerable<string>> GetDistinctTypesAsync()
        {
            return await _ctx.Products
                .AsNoTracking()
                .SelectDistinctTypes()
                .ToListAsync();


        }

    }
}
