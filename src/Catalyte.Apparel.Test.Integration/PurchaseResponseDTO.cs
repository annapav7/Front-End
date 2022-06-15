using Catalyte.Apparel.DTOs.Purchases;
using System;
using System.Collections.Generic;

namespace Catalyte.Apparel.Test.Integration
{
    internal class PurchaseResponseDTO
    {
        public PurchaseResponseDTO()
        {
        }

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public object DeliveryAddress { get; set; }
        public object BillingAddress { get; set; }
        public object CreditCard { get; set; }
        public List<LineItemDTO> LineItems { get; internal set; }
    }
}