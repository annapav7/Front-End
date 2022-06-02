using Catalyte.Apparel.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for promo related service methods.
    /// </summary>
    public interface IPromoProvider
    {
        Task<Promo> GetPromoByIdAsync(int promoId);

        Task<IEnumerable<Promo>> GetPromosAsync();

        Task<Promo> CreatePromoAsync(Promo promo);
    }
}