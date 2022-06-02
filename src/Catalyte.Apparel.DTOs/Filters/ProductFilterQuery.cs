using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalyte.Apparel.DTOs.Filters
{
    /// <summary>
    /// This class is a representation of the URL query parameters used to filter the products endpoint. Null properties are ignored by the filter when it processes this object.
    /// </summary>
    public class ProductFilterQuery
    {
        public string[] Brand { get; set; } = null;
        public string[] Category { get; set; } = null;
        public string[] Demographic { get; set; } = null;
        public decimal? PriceMin { get; set; } = null;
        public decimal? PriceMax { get; set; } = null;
        public string[] Color { get; set; } = null;
        public string[] Material { get; set; } = null;
    }
}
