using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartShop.Models
{
    public class Order
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("shipping_address")]
        public ShippingAddress ShippingAddress { get; set; }
        [JsonProperty("delivery_status")]
        public string DeliveryStatus { get; set; }
        [JsonProperty("status")]
        public string PaidStatus { get; set; }
        [JsonProperty("products")]
        public List<Cart> Products { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount => Products.Sum(p => p.Amount);
    }
}
