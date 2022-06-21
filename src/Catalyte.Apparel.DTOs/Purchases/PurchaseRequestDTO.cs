using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalyte.Apparel.DTOs.Purchases
{
    /// <summary>
    /// Describes a data transfer object for creating a purchase transaction.
    /// </summary>
    public class PurchaseRequestDTO
    {
        [Required] 
        public DateTime OrderDate { get; set; }

        [Required]
        public DeliveryAddressDTO DeliveryAddress { get; set; }

        [Required] 
        public BillingAddressDTO BillingAddress { get; set; }

        [Required]
        public CreditCardDTO CreditCard { get; set; }

        public string CVV { get; set; }
        

        public List<LineItemDTO> LineItems { get; set; }
    }
}
