﻿using Catalyte.Apparel.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalyte.Apparel.Data.Interfaces
{
    /// <summary>
    /// This interface provides an abstraction layer for purchase repository methods.
    /// </summary>
    public interface IPurchaseRepository
    {
        Task<Purchase> CreatePurchaseAsync(Purchase purchase);

        Task<IEnumerable<Purchase>> GetAllPurchasesByEmailAsync(string email);
    }
}
