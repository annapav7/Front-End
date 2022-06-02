using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Catalyte.Apparel.Data.Models
{
    /// <summary>
    /// Describes a promo code available for utilization.
    /// </summary>
    public class Promo : BaseEntity
    {
        [Required(ErrorMessage = "Promo must have a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Promo must have a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Promo must have a type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Promo must have a rate")]
        public short Rate { get; set; }

        [Required(ErrorMessage = "Promo must have a start date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Promo must have a end date")]
        public DateTime? EndDate { get; set; }

    }
}
