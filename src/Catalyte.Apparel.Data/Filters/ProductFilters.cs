using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.DTOs.Filters;
using System.Linq;

namespace Catalyte.Apparel.Data.Filters
{
    /// <summary>
    /// Filter collection for product context queries.
    /// </summary>
    public static class ProductFilters
    {
        public static IQueryable<Product> WhereProductIdEquals(this IQueryable<Product> products, int productId)
        {
            return products.Where(p => p.Id == productId).AsQueryable();
        }
        /// <summary>
        /// Filters Products based on a ProductFilterQuery object.
        /// </summary>
        /// <param name="products">The IQueryable Product object this method is called on.</param>
        /// <param name="filterQuery">The ProductFilterQuery object used to filter the query. Properties that are null will be ignored by the filter.</param>
        /// <returns></returns>
        public static IQueryable<Product> WhereProductMatchesFilterQuery(this IQueryable<Product> products, ProductFilterQuery filterQuery)
        {
            return products
                .Where(p => filterQuery.Brand == null || filterQuery.Brand.Contains(p.Brand))
                .Where(p => filterQuery.Category == null || filterQuery.Category.Contains(p.Category))
                .Where(p => filterQuery.Demographic == null || filterQuery.Demographic.Contains(p.Demographic))
                .Where(p => filterQuery.PriceMin == null || filterQuery.PriceMin <= p.Price)
                .Where(p => filterQuery.PriceMax == null || filterQuery.PriceMax >= p.Price)
                .Where(p => filterQuery.Color == null || (filterQuery.Color.Select(c => string.IsNullOrEmpty(c) ? c : c.ToLower()).Contains(p.PrimaryColorCode.ToLower()) || filterQuery.Color.Select(c => string.IsNullOrEmpty(c) ? c : c.ToLower()).Contains(p.SecondaryColorCode)))
                .Where(p => filterQuery.Material == null || filterQuery.Material.Contains(p.Material))
                .AsQueryable();
        }


        /// <summary>
        /// Takes in products and Filters distinct categories
        /// </summary>
        /// <param name="products"></param>
        /// <returns>collection of strings of distinct categories in DB</returns>
        public static IQueryable<string> SelectDistinctCategories(this IQueryable<Product> products)

        {

            return products.Select(e => e.Category).Distinct().AsQueryable();
        }

        /// <summary>
        /// Takes in products and Filters distinct types
        /// </summary>
        /// <param name="products"></param>
        /// <returns>collection of strings of distinct types in DB</returns>
        public static IQueryable<string> SelectDistinctTypes(this IQueryable<Product> products)

        {

            return products.Select(e => e.Type).Distinct().AsQueryable();
        }
    }
}
