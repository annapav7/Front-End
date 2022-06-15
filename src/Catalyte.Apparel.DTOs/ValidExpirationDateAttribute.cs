using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Catalyte.Apparel.DTOs
{
    /// <summary>
    /// The custom validation method for crdit card expiration date.
    /// </summary>

    public class ValidExpirationDateAttribute : ValidationAttribute

    /// <summary>
    /// If the entered expiry date is before current date than returns invalid.
    /// </summary>
    {
        public override bool IsValid(object value)
        {
            var dash = "/";
            if (value != null)

            {
                var expiryDate = value.ToString();
                if (!expiryDate.Contains(dash)) return false;

                string[] date = Regex.Split(expiryDate, @"(?<=[/])");
                string[] currentDate = Regex.Split(DateTime.Today.ToString("MM/yy"), "/");
                int compareYears = string.Compare(date[1], currentDate[1]);
                int compareMonths = string.Compare(date[0], currentDate[0]);
              
                //if expiration date is in MM/YY format
                if (Regex.Match(expiryDate, @"^(0[1-9]|1[0-2])\/?([0-9]{2})$").Success)
                {
                    //if expiration date is after current date
                    if ((compareYears == 1) || (compareYears == 0 && (compareMonths == 1)) || (compareMonths == 0 && (compareYears == 0)))
                    {
                        return true;
                    }

                }
            }
                return false; 
        }
    }
}


