using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Filters;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Repositories
{
    /// <summary>
    /// This class handles methods for making requests to the purchase repository.
    /// </summary>
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ILogger<PurchaseRepository> _logger;
        private readonly IApparelCtx _ctx;

        public PurchaseRepository(ILogger<PurchaseRepository> logger, IApparelCtx ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        public async Task<Purchase> CreatePurchaseAsync(Purchase purchase)
        {
            await _ctx.Purchases.AddAsync(purchase);
            await _ctx.SaveChangesAsync();

            return purchase;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesByEmailAsync(string email)
        {
            return await _ctx.Purchases
                .AsNoTracking()
                .WhereBillingEmailEquals(email)
                .ToListAsync();
        }
    }
}
