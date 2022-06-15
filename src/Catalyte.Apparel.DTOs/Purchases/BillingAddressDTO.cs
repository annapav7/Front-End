using System.ComponentModel.DataAnnotations;

namespace Catalyte.Apparel.DTOs.Purchases
{
    /// <summary>
    /// Describes a data transfer object for a purchase transaction billing address.
    /// </summary>
    public class BillingAddressDTO
    {
        [Required(ErrorMessage = "{0} is a required field")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string BillingStreet { get; set; }

        [Required(ErrorMessage = "{0} is a required field")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string BillingStreet2 { get; set; }

        [Required(ErrorMessage = "{0} is a required field")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string BillingCity { get; set; }

        public string BillingState { get; set; }

        [Required(ErrorMessage = "{0} is a required field")]
        [DataType(DataType.Text)]
        [MaxLength(10, ErrorMessage = "Zipcode must be in following format: 12345 or 12345-6789")]
        [MinLength(3, ErrorMessage = "Zipcode must be in following format: 12345 or 12345-6789")]
        public int BillingZip { get; set; }

        [Required(ErrorMessage = "{0} is a required field")]
        [DataType(DataType.Text)]
        [RegularExpression(@"(?i)^([a-z.'-]+(?:\s[a-z]\.?)?)\s([a-z'-]+)$", ErrorMessage = "Email must follow format: e.g. example@catalyte.io")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is a required field")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
    }
}
