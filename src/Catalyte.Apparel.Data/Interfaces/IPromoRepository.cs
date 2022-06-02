using Catalyte.Apparel.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for promo repository methods.
    /// </summary>
    public interface IPromoRepository
    {
        Task<Promo> GetPromoByIdAsync(int promoId);

        Task<IEnumerable<Promo>> GetPromosAsync();

        Task<Promo> CreatePromoAsync(Promo promo);

        Task<Promo> GetPromoByTitleAsync(string title);
    }
}