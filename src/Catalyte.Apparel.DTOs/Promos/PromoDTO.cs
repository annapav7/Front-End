using System;

namespace Catalyte.Apparel.DTOs.Promos
{
    /// <summary>
    /// Describes a data transfer object for a promo code.
    /// </summary>
    public class PromoDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public ushort Rate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}