using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.Models
{
    public class Order
    {
        public Order()
        {

        }
        public Order(decimal totalAmount, ShippingAddress shippingAddress, int paymentMethodId, List<Cart> products)
        {
            TotalAmount = totalAmount;
            ShippingAddress = shippingAddress;
            PaymentMethodId = paymentMethodId;
            Products = products;
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }
        [JsonProperty("shipping_address")]
        public ShippingAddress ShippingAddress { get; set; }
        [JsonProperty("payment_method_id")]
        public int PaymentMethodId { get; set; }
        [JsonProperty("products")]
        public List<Cart> Products { get; set; }
    }
}
