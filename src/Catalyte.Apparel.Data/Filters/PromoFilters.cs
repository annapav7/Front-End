using Catalyte.Apparel.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Catalyte.Apparel.Data.Filters
{
    /// <summary>
    /// Filter collection for promo context queries.
    /// </summary>
    public static class PromoFilters
    {
        /// <summary>
        /// This query filters promo by its id.
        /// </summary>
        public static IQueryable<Promo> WherePromoIdEquals(this IQueryable<Promo> promos, int promoId)
        {
            return promos.Where(p => p.Id == promoId).AsQueryable();
        }

        /// <summary>
        /// This query filters promo by its title.
        /// </summary>
        public static IQueryable<Promo> WherePromoTitleEquals(this IQueryable<Promo> promos, string promoTitle)
        {
            return promos.Where(p => p.Title == promoTitle).AsQueryable();
        }
    }
}