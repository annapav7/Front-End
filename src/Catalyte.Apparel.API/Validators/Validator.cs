using System;
using System.Linq;
using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;

namespace Catalyte.Apparel.API.Validators
{
    public static class Validator
    {
        /// <summary>
        /// Validates that each field in the given promo passes requirements for validation. Throws exceptions if promo is invalid.
        /// </summary>
        /// <param name="promo">The promo being validated.</param>
        /// <returns></returns>
        public static void ValidatePromo(this Promo promo)
        {
            // CHECK THAT TITLE IS CAPITAL LETTERS AND NUMBERS ONLY
            if (promo.Title.Any(char.IsLower) || !(promo.Title.Any(char.IsLetterOrDigit)) || promo.Title.Any(char.IsPunctuation) || promo.Title.Trim().Contains(" "))
            {
                throw new BadRequestException("Promo title must only contain capital letters and numbers and no spaces");
            }

            // CHECK IF PROMO TYPE IS VALID
            if (!(promo.Type.Trim().ToLower() == "flat" | promo.Type.Trim().ToLower() == "percent"))
            {
                throw new BadRequestException("Promo type must be flat or percent");
            }

            // CHECK THAT PERCENT IS BETWEEN 1-100
            if (promo.Type.Trim().ToLower() == "percent" && !(promo.Rate >= 1 && promo.Rate <= 100))
            {
                throw new BadRequestException("Promo percentage rate must be between 1 and 100");
            }

            // CHECK THAT RATE IS NOT NEGATIVE
            if (promo.Rate < 0)
            {
                throw new BadRequestException("Promo rate must be positive");
            }

            // CHECK THAT START DATE ISN'T LATER THAN END DATE
            if (promo.StartDate > promo.EndDate)
            {
                throw new BadRequestException("Promo cannot end before it starts");
            }

            // CHECK THAT END DATE ISN'T IN THE PAST
            if (promo.EndDate < DateTime.Now)
            {
                throw new BadRequestException("Promo cannot end before it's created");
            }

            // Clean up the title and type
            promo.Title = promo.Title.Trim();
            promo.Type = promo.Type.Trim().ToLower();
        }
    }
}
