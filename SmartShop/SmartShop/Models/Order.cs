using Newtonsoft.Json;
using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
=======
using System.Linq;
>>>>>>> Stashed changes

namespace SmartShop.Models
{
    public class Order
    {
        [JsonProperty("id")]
        public int Id { get; set; }
<<<<<<< Updated upstream
        [JsonProperty("paid")]
        public decimal TotalAmount { get; set; }
=======
>>>>>>> Stashed changes
        [JsonProperty("shipping_address")]
        public ShippingAddress ShippingAddress { get; set; }
        [JsonProperty("delivery_status")]
        public string DeliveryStatus { get; set; }
<<<<<<< Updated upstream
=======
        [JsonProperty("status")]
        public string PaidStatus { get; set; }
>>>>>>> Stashed changes
        [JsonProperty("products")]
        public List<Cart> Products { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
<<<<<<< Updated upstream
=======
        public decimal TotalAmount => Products.Sum(p => p.Amount);
>>>>>>> Stashed changes
    }
}
