using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.Models
{
    public class Brand
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        public Uri Img { get => new Uri($"{Config.StorageAddress}{Image}"); }
        public bool IsActive { get; set; }
        [JsonProperty("products_count")]
        public int ProductsCount { get; set; }

        [JsonProperty("products")]
        public List<Product> Products { get; set; }
    }
}
