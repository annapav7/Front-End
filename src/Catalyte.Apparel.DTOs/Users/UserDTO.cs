﻿using System;

namespace Catalyte.Apparel.DTOs
{
    /// <summary>
    /// Describes a data transfer object for a user.
    /// </summary>
    public class UserDTO
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ShippingStreet { get; set; }

        public string ShippingStreet2 { get; set; }

        public string ShippingCity { get; set; }

        public string ShippingState { get; set; }

        public int ShippingZip { get; set; }
    }
}
