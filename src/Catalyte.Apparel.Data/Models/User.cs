using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Catalyte.Apparel.Data.Models
{
    /// <summary>
    /// Describes a sports apparel site user.
    /// </summary>
    public class User : BaseEntity
    {
        [JsonRequired]
        public string Email { get; set; }

        public string Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ShippingStreet { get; set; }

        public string ShippingStreet2 { get; set; }

        public string ShippingCity { get; set; }

        public string ShippingState { get; set; }

        public int ShippingZip { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static IEqualityComparer<User> ProductComparer { get; } = new ProductEqualityComparer();

        private sealed class ProductEqualityComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Email == y.Email && x.Role == y.Role && x.FirstName == y.FirstName && x.LastName == y.LastName && x.ShippingStreet == y.ShippingStreet && x.ShippingStreet2 == y.ShippingStreet2 && x.ShippingCity == y.ShippingCity && x.ShippingState == y.ShippingState && x.ShippingZip == y.ShippingZip;
            }

            public int GetHashCode(User obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.Email);
                hashCode.Add(obj.Role);
                hashCode.Add(obj.FirstName);
                hashCode.Add(obj.LastName);
                hashCode.Add(obj.ShippingStreet);
                hashCode.Add(obj.ShippingStreet2);
                hashCode.Add(obj.ShippingCity);
                hashCode.Add(obj.ShippingState);
                hashCode.Add(obj.ShippingZip);
                return hashCode.ToHashCode();
            }
        }
    }
}
