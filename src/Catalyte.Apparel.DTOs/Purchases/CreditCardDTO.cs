using System.ComponentModel.DataAnnotations;

namespace Catalyte.Apparel.DTOs.Purchases
{
    /// <summary>
    /// Describes a data transfer object for a credit card.
    /// </summary>
    public class CreditCardDTO
    {
        [Required(ErrorMessage = "{0} is a required field")] 
        [DataType(DataType.CreditCard)]
        [MinLength(16, ErrorMessage = "Card number must be a valid Visa or Mastercard number")]
        [MaxLength(16, ErrorMessage = "Card number must be a valid Visa or Mastercard number")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Card number must be a valid Visa or Mastercard number")]
        public string CardNumber { get; set; }


        [Required(ErrorMessage = "{0} is a required field")]
        [DataType(DataType.CreditCard)]
        [MinLength(3, ErrorMessage = "CVV must be three consecutive digits")]
        [MaxLength(3, ErrorMessage = "CVV must be three consecutive digits")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "CVV must be three consecutive digits")]
        public string CVV { get; set; }


        [Required(ErrorMessage = "{0} is a required field")]
        [DataType(DataType.CreditCard)]
        [StringLength(5, ErrorMessage = "Card Expiration must be in the following format: mm/yy")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$", ErrorMessage = "Card Expiration must be in the following format: mm/yy")]
        [ValidExpirationDate(ErrorMessage = "Card is expired.")]
        public string Expiration { get; set; }
         

        [Required(ErrorMessage = "{0} is a required field")]
        [DataType(DataType.Text)] 
        [RegularExpression(@"(?i)^([a-z.'-]+(?:\s[a-z]\.?)?)\s([a-z'-]+)$", ErrorMessage = "Cardholder must match the name on your Visa or Mastercard: First M Last or First Last")]
        public string CardHolder { get; set; }
      

    }
}
