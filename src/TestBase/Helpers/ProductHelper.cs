using Catalyte.Apparel.DTOs.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalyte.Apparel.TestBase.Helpers
{
    public class ProductHelper
    {
        // brands
        public static string brandNike = "Nike";
        public static string brandAdidas = "Adidas";

        // categories
        public static string categorySoccer = "Soccer";
        public static string categoryGolf = "Golf";

        // color codes
        public static string colrHexWhite = "#ffffff";
        public static string colorHexBlack = "#000000";

        // demographics
        public static string demographicMen = "Men";
        public static string demographicWomen = "Women";

        // materials
        public static string materialCotton = "Cotton";
        public static string materialPolyester = "Polyester";

        // types
        public static string typesHelmet = "Helmet";
        public static string typesBelt = "Belt";


        public static ProductFilterQuery GenerateValidProductFilterQuery()
        {
            return new ProductFilterQuery()
            {
                Brand = new string[] { brandNike, brandAdidas },
                Category = new string[] { categorySoccer, categoryGolf },
                Demographic = new string[] { demographicWomen, demographicMen },
                PriceMin = 1m,
                PriceMax = 100m,
                Color = new string[] { colrHexWhite, colorHexBlack },
                Material = new string[] { materialCotton, materialPolyester }
            };
        }
    }
}
