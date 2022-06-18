using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalyte.Apparel.Data.Models
{
    /// <summary>
    /// Describes a purchase object that holds the information for a transaction.
    /// </summary>
    public class Purchase : BaseEntity
    {
        public DateTime OrderDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string BillingStreet { get; set; }

        [MaxLength(200)]
        public string BillingStreet2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string BillingCity { get; set; }


        [Required]
        [MaxLength(2)]
        public string BillingState { get; set; }

        [Required]
        [MaxLength(10)]
        public string BillingZip { get; set; }

        [Required]
        [MaxLength(100)]
        public string BillingEmail { get; set; }

        [Required]
        [MaxLength(15)]
        public string BillingPhone { get; set; }

        [Required]
        [MaxLength(50)]
        public string DeliveryFirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string DeliveryLastName { get; set; }

        [Required]
        [MaxLength(200)]
        public string DeliveryStreet { get; set; }

        [MaxLength(200)]
        public string DeliveryStreet2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string DeliveryCity { get; set; }

        [Required]
        [MaxLength(2)]
        public string DeliveryState { get; set; }

        [Required]
        [MaxLength(10)]
        public string DeliveryZip { get; set; }

        [MaxLength(16)]
        [Required]
        public string CardNumber { get; set; }

        [Required]
        [MaxLength(3)]
        public string CVV { get; set; }

        [Required]
        [MaxLength(5)]
        public string Expiration { get; set; }

        [Required]
        [MaxLength(32)]
        public string CardHolder { get; set; }

        public ICollection<LineItem> LineItems { get; set; }
    }
}
