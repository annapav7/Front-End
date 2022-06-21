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

<<<<<<< HEAD
        [Required]
        [MaxLength(200)]
=======
        [MaxLength(100)]
        [Required]
>>>>>>> d6a564977e126d491c1fe426815941279b801549
        public string BillingStreet { get; set; }

        [MaxLength(200)]
        public string BillingStreet2 { get; set; }

        [Required]
        [MaxLength(50)]
        [Required]
        public string BillingCity { get; set; }


        [Required]
        [MaxLength(2)]
        [Required]
        public string BillingState { get; set; }

<<<<<<< HEAD
        [Required]
        [MaxLength(10)]
=======
        [MaxLength(5)]
        [Required]
>>>>>>> d6a564977e126d491c1fe426815941279b801549
        public string BillingZip { get; set; }

        [Required]
        [MaxLength(100)]
        [Required]
        public string BillingEmail { get; set; }

<<<<<<< HEAD
        [Required]
        [MaxLength(15)]
=======
        [MaxLength(12)]
        [Required]
>>>>>>> d6a564977e126d491c1fe426815941279b801549
        public string BillingPhone { get; set; }

        [Required]
        [MaxLength(50)]
        [Required]
        public string DeliveryFirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Required]
        public string DeliveryLastName { get; set; }

<<<<<<< HEAD
        [Required]
        [MaxLength(200)]
=======
        [MaxLength(100)]
        [Required]
>>>>>>> d6a564977e126d491c1fe426815941279b801549
        public string DeliveryStreet { get; set; }

        [MaxLength(200)]
        public string DeliveryStreet2 { get; set; }

        [Required]
        [MaxLength(50)]
        [Required]
        public string DeliveryCity { get; set; }

        [Required]
        [MaxLength(2)]
        [Required]
        public string DeliveryState { get; set; }

        [Required]
        [MaxLength(10)]
<<<<<<< HEAD
        public string DeliveryZip { get; set; }
=======
        [Required]
        public int DeliveryZip { get; set; }
>>>>>>> d6a564977e126d491c1fe426815941279b801549

        [MaxLength(16)]
        [Required]
        public string CardNumber { get; set; }

<<<<<<< HEAD
        [Required]
        [MaxLength(3)]
        public string CVV { get; set; }
=======
        [MaxLength(3)]
        [Required]
        public int CVV { get; set; }
>>>>>>> d6a564977e126d491c1fe426815941279b801549

        [Required]
        [MaxLength(5)]
        [Required]
        public string Expiration { get; set; }

<<<<<<< HEAD
        [Required]
        [MaxLength(32)]
=======
        [MaxLength(100)]
        [Required]
>>>>>>> d6a564977e126d491c1fe426815941279b801549
        public string CardHolder { get; set; }

        public ICollection<LineItem> LineItems { get; set; }
    }
}
