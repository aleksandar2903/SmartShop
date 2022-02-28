using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Uri Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Subcategory Subcategory { get; set; }
        public int SubcategoryId { get; set; }
        public List<Image> Images { get; set; }
    }
}
