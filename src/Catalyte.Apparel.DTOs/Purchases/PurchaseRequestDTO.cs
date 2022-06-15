﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalyte.Apparel.DTOs.Purchases
{
    /// <summary>
    /// Describes a data transfer object for creating a purchase transaction.
    /// </summary>
    public class PurchaseRequestDTO
    {
        public DateTime OrderDate { get; set; }

        public DeliveryAddressDTO DeliveryAddress { get; set; }

        public BillingAddressDTO BillingAddress { get; set; }

        [Required]
        public CreditCardDTO CreditCard { get; set; }

        public List<LineItemDTO> LineItems { get; set; }
    }
}
