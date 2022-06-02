using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.DTOs.Products;
using Catalyte.Apparel.DTOs.Filters;
using Catalyte.Apparel.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for product related service methods.
    /// </summary>
    public interface IProductProvider
    { 
        Task<Product> GetProductByIdAsync(int productId);

        Task<IEnumerable<Product>> GetProductsAsync(ProductFilterQuery filterQuery);


        Task<IEnumerable<string>> GetDistinctCategoriesAsync();

        Task<IEnumerable<string>> GetDistinctTypesAsync();

    }
}
