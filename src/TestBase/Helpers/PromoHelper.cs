using Catalyte.Apparel.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalyte.Apparel.TestBase.Helpers
{
    public class PromoHelper
    {

        public static Promo GenerateValidFlatPromo()
        {
            return new Promo()
            {
                Title = "TEST1",
                Description = "This is a valid test",
                Type = "flat",
                Rate = 50,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };
        }

    }
}
