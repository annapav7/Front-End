using Catalyte.Apparel.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalyte.Apparel.Data.SeedData
{
    /// <summary>
    /// This class provides tools for generating random promos.
    /// </summary>
    public class PromoFactory
    {
        Random _rand = new();

        private List<string> _titles = new()
        {
            "SUMMERSALE",
            "NEWUSERDISCOUNT",
            "SALE4EVER",
            "WINTERSALE",
            "BOGO20",
            "SECRETCODE",
            "RANDOM",
            "THISISATEST",
            "ANOTHAONE",
            "CATALYTEROCKS",
            "123",
            "321",
            "TEST123",
            "THERESASPACEINTHISONE ",
            "FREESTUFFMYGUY"
        };

        private readonly List<string> _description = new()
        {
            "This is a description",
            "There can be more than one promo with the same description",
            "This is a test"
        };
        private readonly List<string> _type = new()
        {
            "flat",
            "percent",
            "  PeRcEnT ",
            " rATe"
        };

        /// <summary>
        /// Generates a number of random promos based on input.
        /// </summary>
        /// <param name="numberOfPromos">The number of random promos to generate.</param>
        /// <returns>A list of random promos.</returns>
        public List<Promo> GenerateRandomPromos(int numberOfPromos)
        {

            var promoList = new List<Promo>();

            for (var i = 0; i < numberOfPromos; i++)
            {
                promoList.Add(CreateRandomPromo(i + 1));
            }

            return promoList;
        }

        /// <summary>
        /// Uses random generators to build a promos.
        /// </summary>
        /// <param name="id">ID to assign to the promo.</param>
        /// <returns>A randomly generated promo.</returns>
        private Promo CreateRandomPromo(int id)
        {
            return new Promo
            {
                Id = id,
                Title = _titles[_rand.Next(0, 14)],
                Description = _description[_rand.Next(0, 2)],
                Type = _type[_rand.Next(0, 3)],
                Rate = (short) _rand.Next(1, 100),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };
        }
    }
}