using Catalyte.Apparel.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Providers.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for purchase related service methods.
    /// </summary>
    public interface IPurchaseProvider
    {
        Task<IEnumerable<Purchase>> GetAllPurchasesByEmailAsync(string email);

        void GetAllPurchasesAsync();

        Task<Purchase> CreatePurchaseAsync(Purchase model);
    }
}
