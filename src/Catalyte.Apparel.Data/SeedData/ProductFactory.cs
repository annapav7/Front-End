using Catalyte.Apparel.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalyte.Apparel.Data.SeedData
{
    /// <summary>
    /// This class provides tools for generating random products.
    /// </summary>
    public class ProductFactory
    {
        Random _rand = new();

        // add lists of BRAND, MATERIAL,*
        // create get random brand and material methods*
        // set default test IMG SRC,*
        // create PRICE, QUANTITY random generator Price between 2 and 200 inclusive. quantity 1-1000 inclusive
        //create name generator
        // create description generator


        private List<string> _materials = new()
        {

            "Cotton",
            "Wool",
            "Silk",
            "Leather",
            "Synythetic",
            "Velvet",
            "Satin",
            "Linen",
            "Denim",
            "Mithril",
            "Steel Plate",
            "Kevlar",
            "Solid Gold",
            "Heart Pine",
            "Hand Blown Glass",
            "Plastic",
            "Cement"
        };

        private List<string> _brands = new()
        {
            "Patagonia",
            "Merrell",
            "Altra",
            "Lowa",
            "Five Ten",
            "Teva",
            "Keen",
            "Nike",
            "Adidas",
            "Reebok",
            "New Balance"

        };

        private List<string> _colors = new()
        {
            "#000000", // white
            "#ffffff", // black
            "#39add1", // light blue
            "#3079ab", // dark blue
            "#c25975", // mauve
            "#e15258", // red
            "#f9845b", // orange
            "#838cc7", // lavender
            "#7d669e", // purple
            "#53bbb4", // aqua
            "#51b46d", // green
            "#e0ab18", // mustard
            "#637a91", // dark gray
            "#f092b0", // pink
            "#b7c0c7"  // light gray
        };

        private readonly List<string> _demographics = new()
        {
            "Men",
            "Women",
            "Kids"
        };
        private readonly List<string> _categories = new()
        {
            "Golf",
            "Soccer",
            "Basketball",
            "Hockey",
            "Football",
            "Running",
            "Baseball",
            "Skateboarding",
            "Boxing",
            "Weightlifting"
        };

        private List<string> _adjectives = new()
        {
            "Lightweight",
            "Slim",
            "Shock Absorbing",
            "Exotic",
            "Elastic",
            "Fashionable",
            "Trendy",
            "Next Gen",
            "Colorful",
            "Comfortable",
            "Water Resistant",
            "Wicking",
            "Heavy Duty",
            "Bullet Proof",
            "Magic",
            "Flame Retardant",
            "Scratchy",
            "Razor Sharp",
            "Bulky",
            "Shiny",
            "Profitable"
        };

        private List<string> _types = new()
        {
            "Pant",
            "Short",
            "Shoe",
            "Glove",
            "Jacket",
            "Tank Top",
            "Sock",
            "Sunglasses",
            "Hat",
            "Helmet",
            "Belt",
            "Visor",
            "Shin Guard",
            "Elbow Pad",
            "Headband",
            "Wristband",
            "Hoodie",
            "Flip Flop",
            "Pool Noodle",
            "Banana Stand"
        };

        private List<string> _skuMods = new()
        {
            "Blue",
            "Red",
            "KJ",
            "SM",
            "RD",
            "LRG",
            "SM"
        };

        /// <summary>
        /// Generates a random date
        /// </summary>
        /// <returns>A datetime</returns>
        private DateTime GetRandomDate()
        {
         int rndYear = _rand.Next(2015, 2023);
         int rndMonth = _rand.Next(1, 12);
         int rndDay = (DateTime.IsLeapYear(rndYear)) ? _rand.Next(1,30) : _rand.Next(1,29);
            return new DateTime(rndYear, rndMonth, rndDay);
        }

       

        /// <summary>
        /// Generates a randomized product SKU.
        /// </summary>
        /// <returns>A SKU string.</returns>
        private string GetRandomSku()
        {
            var builder = new StringBuilder();
            builder.Append(RandomString(3));
            builder.Append('-');
            builder.Append(RandomString(3));
            builder.Append('-');
            builder.Append(_skuMods[_rand.Next(0, 6)]);

            return builder.ToString().ToUpper();
        }


        /// <summary>
        /// Returns a random quantity .
        /// </summary>
        /// <returns>int quantity.</returns>
        private int GetQuantity()
        {
            return _rand.Next(1, 1000);
        }

        /// <summary>
        /// Returns a random price .
        /// </summary>
        /// <returns>decimal price.</returns>
        private decimal GetPrice()
        {
            return _rand.Next(5, 200);
        }


        /// <summary>
        /// Returns a random adjective .
        /// </summary>
        /// <returns>adjective string.</returns>
        private string GetAdjective()
        {
            return _adjectives[_rand.Next(0, 20)];
        }

        /// <summary>
        /// Returns a random brand .
        /// </summary>
        /// <returns>brand string.</returns>
        private string GetBrand()
        {
            return _brands[_rand.Next(0, 10)];
        }



        /// <summary>
        /// Returns a random material.
        /// </summary>
        /// <returns>string material.</returns>
        private string GetMaterial()
        {
            return _materials[_rand.Next(0, 16)];
        }




        /// <summary>
        /// Returns a random color code.
        /// </summary>
        /// <returns>string hex color code.</returns>
        private string GetColor()
        {
            return _colors[_rand.Next(0, 14)];
        }

        /// <summary>
        /// Returns a random bool.
        /// </summary>
        /// <returns>A bool.</returns>
        private bool GetBool()
        {
            return _rand.Next(0,2) > 0;
        }

        /// <summary>
        /// Returns a random type from the list of types.
        /// </summary>
        /// <returns>A type string.</returns>
        private string GetType()
        {
            return _types[_rand.Next(0, 18)];
        }

        /// <summary>
        /// Returns a random demographic from the list of demographics.
        /// </summary>
        /// <returns>A demographic string.</returns>
        private string GetDemographic()
        {
            return _demographics[_rand.Next(0, 3)];
        }

        /// <summary>
        /// Returns a random category from the list of categories.
        /// </summary>
        /// <returns>A category string.</returns>
        private string GetCategory()
        {
            return _categories[_rand.Next(0, 9)];
        }

        /// <summary>
        /// Generates a random product offering id.
        /// </summary>
        /// <returns>A product offering string.</returns>
        private string GetRandomProductId()
        {
            return "po-" + RandomString(7);
        }

        /// <summary>
        /// Generates a random style code.
        /// </summary>
        /// <returns>A style code string.</returns>
        private string GetStyleCode()
        {
            return "sc" + RandomString(5);
        }

        /// <summary>
        /// Generates a number of random products based on input.
        /// </summary>
        /// <param name="numberOfProducts">The number of random products to generate.</param>
        /// <returns>A list of random products.</returns>
        public List<Product> GenerateRandomProducts(int numberOfProducts)
        {

            var productList = new List<Product>();

            for (var i = 0; i < numberOfProducts; i++)
            {
                productList.Add(CreateRandomProduct(i + 1));
            }

            return productList;
        }

        /// <summary>
        /// Uses random generators to build a products.
        /// </summary>
        /// <param name="id">ID to assign to the product.</param>
        /// <returns>A randomly generated product.</returns>
        private Product CreateRandomProduct(int id)
        {
            var cat = GetCategory();
            var adj = GetAdjective();
            var type = GetType();
            var demographics = GetDemographic();
            var name = String.Concat(adj, " ", cat, " ", type);
            var description = String.Concat(name, " is a ", cat.ToLower()," ", type.ToLower(), " especially designed for ", demographics.ToLower(), "! It's really ", adj.ToLower(), "!");


            return new Product
            {
                Id = id,
                Category = cat,
                Type = type,
                Sku = GetRandomSku(),
                Demographic = GetDemographic(),
                GlobalProductCode = GetRandomProductId(),
                StyleNumber = GetStyleCode(),
                ReleaseDate = GetRandomDate(),
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
                Active = GetBool(),
                PrimaryColorCode = GetColor(),
                SecondaryColorCode = GetColor(),
                ImageSrc = "https://source.unsplash.com/random",
                Brand = GetBrand(),
                Material = GetMaterial(),
                Price = GetPrice(),
                Quantity = GetQuantity(),
                Name = name,
                Description = description
               
            };
        }

        /// <summary>
        /// Generates a random string of characters.
        /// </summary>
        /// <param name="size">Number of characters in the string.</param>
        /// <param name="lowerCase">Boolean if the character string should be lowercase only; defaults to false.</param>
        /// <returns>A random string of characters.</returns>
        private string RandomString(int size, bool lowerCase = false)
        {

            // ** Learning moment **
            // Code From
            // https://www.c-sharpcorner.com/article/generating-random-number-and-string-in-C-Sharp/

            // ** Learning moment **
            // Always use a string builder when concatenating more than a couple of strings.
            // Why? https://www.geeksforgeeks.org/c-sharp-string-vs-stringbuilder/
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                // ** Learning moment **
                // Because 'char' is a reserved word you can put '@' at the beginning to allow
                // its use as a variable name.  You could do the same thing with 'class'
                var @char = (char)_rand.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
