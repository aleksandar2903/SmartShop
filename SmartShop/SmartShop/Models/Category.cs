using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }

        public List<Subcategory> Subcategories { get; set; }
    }
}
