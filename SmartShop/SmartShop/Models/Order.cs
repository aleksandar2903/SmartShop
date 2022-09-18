using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public PaymentInformations PaymentInformations { get; set; }
        public List<Product> Products  { get; set; }
    }
}
