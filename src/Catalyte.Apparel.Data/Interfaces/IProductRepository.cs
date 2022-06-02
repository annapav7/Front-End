using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.DTOs.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for product repository methods.
    /// </summary>
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int productId);

        Task<IEnumerable<Product>> GetProductsAsync(ProductFilterQuery filterQuery);
  
        Task<IEnumerable<string>> GetDistinctCategoriesAsync();

        Task<IEnumerable<string>> GetDistinctTypesAsync();
    }
}
