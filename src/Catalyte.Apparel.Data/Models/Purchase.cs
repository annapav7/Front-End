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

        [MaxLength(100)]
        [Required]
        public string BillingStreet { get; set; }

        [MaxLength(100)]
        public string BillingStreet2 { get; set; }

        [MaxLength(50)]
        [Required]
        public string BillingCity { get; set; }

        [MaxLength(2)]
        [Required]
        public string BillingState { get; set; }

        [MaxLength(5)]
        [Required]
        public string BillingZip { get; set; }

        [MaxLength(100)]
        [Required]
        public string BillingEmail { get; set; }

        [MaxLength(12)]
        [Required]
        public string BillingPhone { get; set; }

        [MaxLength(50)]
        [Required]
        public string DeliveryFirstName { get; set; }

        [MaxLength(50)]
        [Required]
        public string DeliveryLastName { get; set; }

        [MaxLength(100)]
        [Required]
        public string DeliveryStreet { get; set; }

        [MaxLength(100)]
        public string DeliveryStreet2 { get; set; }

        [MaxLength(50)]
        [Required]
        public string DeliveryCity { get; set; }

        [MaxLength(2)]
        [Required]
        public string DeliveryState { get; set; }

        [MaxLength(10)]
        [Required]
        public int DeliveryZip { get; set; }

        [MaxLength(16)]
        [Required]
        public string CardNumber { get; set; }

        [MaxLength(3)]
        [Required]
        public int CVV { get; set; }

        [MaxLength(5)]
        [Required]
        public string Expiration { get; set; }

        [MaxLength(100)]
        [Required]
        public string CardHolder { get; set; }

        public ICollection<LineItem> LineItems { get; set; }
    }
}
