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
    }
}
