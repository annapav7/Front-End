using Catalyte.Apparel.Data.Context;
using Catalyte.Apparel.Data.Filters;
using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Repositories
{
    /// <summary>
    /// This class handles methods for making requests to the promo repository.
    /// </summary>
    public class PromoRepository : IPromoRepository
    {
        private readonly IApparelCtx _ctx;

        public PromoRepository(IApparelCtx ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// This query searches the database for a promo code matching the given id and returns it.
        /// </summary>
        /// <param name="promoId">An integer matching the id of a promo code in the database.</param>
        /// <returns>The promo with the id matching the integer parameter.</returns>
        public async Task<Promo> GetPromoByIdAsync(int promoId)
        {
            return await _ctx.Promos
                .AsNoTracking()
                .WherePromoIdEquals(promoId)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// This query retrieves all promo codes from the database and returns them.
        /// </summary>
        public async Task<IEnumerable<Promo>> GetPromosAsync()
        {
            return await _ctx.Promos
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Given a new Promo object, this query adds it to the database.
        /// </summary>
        /// <param name="promo">A Promo object with all required fields filled out.</param>
        /// <returns></returns>
        public async Task<Promo> CreatePromoAsync(Promo promo)
        {
            await _ctx.Promos.AddAsync(promo);
            await _ctx.SaveChangesAsync();

            return promo;
        }

        /// <summary>
        /// This query searches the database for a promo code matching the given title and returns it.
        /// </summary>
        /// <param name="promoTitle">A string matching the title of a promo code in the database.</param>
        /// <returns>The promo with the title matching the integer parameter.</returns>
        public async Task<Promo> GetPromoByTitleAsync(string promoTitle)
        {
            return await _ctx.Promos
                .AsNoTracking()
                .WherePromoTitleEquals(promoTitle)
                .SingleOrDefaultAsync();
        }
    }
}