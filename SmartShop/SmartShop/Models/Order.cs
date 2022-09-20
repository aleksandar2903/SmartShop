using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SmartShop.Models
{
    public class Order
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("paid")]
        public decimal TotalAmount { get; set; }
        [JsonProperty("shipping_address")]
        public ShippingAddress ShippingAddress { get; set; }
        [JsonProperty("delivery_status")]
        public string DeliveryStatus { get; set; }
        [JsonProperty("products")]
        public List<Cart> Products { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
