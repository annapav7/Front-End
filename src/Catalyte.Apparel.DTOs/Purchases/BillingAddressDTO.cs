using System.ComponentModel.DataAnnotations;

namespace Catalyte.Apparel.DTOs.Purchases
{
    /// <summary>
    /// Describes a data transfer object for a purchase transaction billing address.
    /// </summary>
    public class BillingAddressDTO
    {
<<<<<<< HEAD

        [Required(ErrorMessage = "Street address is a required field.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"(?i)^([a-z0-9.'-]+(?:\s[a-z0-9]\.?)?)\s([a-z0-9'-]+)$", ErrorMessage = "Street address must be a combination of letters, numbers, and 1 whitespace between only.")]
        public string BillingStreet { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression(@"(?i)^([a-z0-9.'-]+(?:\s[a-z0-9]\.?)?)\s([a-z0-9'-]+)$", ErrorMessage = "Street address2 must be a combination of letters, numbers, and 1 whitespace between only.")]
        public string BillingStreet2 { get; set; }

        [Required(ErrorMessage = "City is a required field")]
        [DataType(DataType.Text)]
        [RegularExpression(@"(?i)^([a-z.'-+&*%#@!^]+(?:\s[a-z]\.?)?)([a-z.'-+&*%#@!^]+(?:\s[a-z]\.?)?)\s([a-z'-+&*%#@!^]+)$", ErrorMessage = "City must be a combination of letters, numbers, 1 whitespace between or any special characters.")]
        public string BillingCity { get; set; }   
=======
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
>>>>>>> d6a564977e126d491c1fe426815941279b801549

        [Required(ErrorMessage = "Please select a state.")]
        [DataType(DataType.Text)]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State field must must be 2 capital letters.")]
        [RegularExpression(@"^([A-Z]+)$", ErrorMessage = "State field must be capital letters only.")]
        public string BillingState { get; set; }

<<<<<<< HEAD
        [Required(ErrorMessage = "Zipcode is a required field")]
        [DataType(DataType.PostalCode)]
        [RegularExpression("^[0-9]{5}(?:-[0-9]{4})?$", ErrorMessage = "Zipcode format must be: 12345 or 12345-6789")]      
        public string BillingZip { get; set; }

        [Required(ErrorMessage = "Email is a required field")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9\.\-]+)@([a-zA-Z0-9]+)((\.(\w){3})+)$", ErrorMessage = "Email must follow format: {customer@v4.com}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is a required field")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[ -. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone number format must be: (124)-345-6789 or (124) 345-6789")]
=======
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
>>>>>>> d6a564977e126d491c1fe426815941279b801549
        public string Phone { get; set; }
    }
}
