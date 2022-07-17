using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sub_products_count")]
        public string ProductsCount { get; set; }
        [JsonProperty("image")]
        public string Img { get; set; }

        [JsonProperty("subcategories")]
        public List<Subcategory> Subcategories { get; set; }
    }
}
