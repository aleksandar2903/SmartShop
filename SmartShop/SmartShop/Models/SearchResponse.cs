using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.Models
{
    public class SearchResponse
    {
        [JsonProperty("products")]
        public Root<Product> Products { get; set; }
        [JsonProperty("categories")]
        public List<Subcategory> Categories { get; set; }
        [JsonProperty("brands")]
        public List<Brand> Brands { get; set; }
    }
}
