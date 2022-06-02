using Catalyte.Apparel.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Catalyte.Apparel.Data.Filters
{
    /// <summary>
    /// Filter collection for product context queries.
    /// </summary>
    public static class PurchaseFilters
    {
        public static IQueryable<Purchase> WhereBillingEmailEquals(this IQueryable<Purchase> purchases, string email)
        {
            return purchases.Where(p => p.BillingEmail == email).AsQueryable();
        }
    }
}
