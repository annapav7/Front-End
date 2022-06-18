using System.ComponentModel.DataAnnotations;

namespace Catalyte.Apparel.DTOs.Purchases
{
    /// <summary>
    /// Describes a data transfer object for a purchase transaction delivery address.
    /// </summary>
    public class DeliveryAddressDTO
    {
        [Required(ErrorMessage = "First Name is a required field.")]
        [DataType(DataType.Text)]
        [MinLength(3, ErrorMessage = "First name must have at least three characters.")]
        [RegularExpression(@"(?i)^([a-z.'-]+(?:\s[a-z]\.?)?)\s([a-z.'-]+)$", ErrorMessage = "First Name may not have multiple spaces in a row, and must not contain any leading or trailing white space.")]
        public string DeliveryFirstName { get; set; }

        [Required(ErrorMessage = "Last Name is a required field.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^([a-zA-Z]+)$", ErrorMessage = "Last Name must be a combination of letters only.")]
        public string DeliveryLastName { get; set; }

        [Required(ErrorMessage = "Street Address is a required field.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"(?i)^([a-z0-9.'-]+(?:\s[a-z0-9]\.?)?)\s([a-z0-9'-]+)$", ErrorMessage = "Street Address must be a combination of letters, numbers, and 1 whitespace between only.")]
        public string DeliveryStreet { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression(@"(?i)^([a-z0-9.'-]+(?:\s[a-z0-9]\.?)?)\s([a-z0-9'-]+)$", ErrorMessage = "Street Address2 must be a combination of letters, numbers, and 1 whitespace between only.")]
        public string DeliveryStreet2 { get; set; }

        [Required(ErrorMessage = "City  is a required field.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"(?i)^([a-z.'-+&*%#@!^]+(?:\s[a-z]\.?)?)([a-z.'-+&*%#@!^]+(?:\s[a-z]\.?)?)\s([a-z'-+&*%#@!^]+)$", ErrorMessage = "City must be a combination of letters, numbers, 1 whitespace between or any special characters.")]
        public string DeliveryCity { get; set; }

        [Required(ErrorMessage = "Please select a state.")]
        [DataType(DataType.Text)]
        [StringLength(2,MinimumLength = 2, ErrorMessage = "State field must must be 2 capital letters only.")]
        [RegularExpression(@"^([A-Z]+)$", ErrorMessage = "State field must be capital letters only.")]
        public string DeliveryState { get; set; }

        [Required(ErrorMessage = "Zip is a required field")]
        [DataType(DataType.PostalCode)]
        [RegularExpression("^[0-9]{5}(?:-[0-9]{4})?$", ErrorMessage = "Zipcode format must be: 12345 or 12345-6789")]
        public string DeliveryZip { get; set; }
    }
}
