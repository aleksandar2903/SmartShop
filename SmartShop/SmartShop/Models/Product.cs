using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartShop.Models
{
    public class Product : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("image")]
        public Image Image { get => _image; set { _image = value; OnPropertyChanged(); } }
        Image _image;
        bool _favourite;
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("brand")]
        public Brand Brand { get; set; }
        [JsonProperty("favourite")]
        public bool Favourite { get => _favourite; set { _favourite = value; OnPropertyChanged(); } }
        [JsonProperty("similar_product")]
        public Product SimilarProduct { get; set; }
        [JsonProperty("similar_products")]
        public List<Product> SimilarProducts { get; set; }
        [JsonProperty("popular_brands")]
        public List<Product> PopularProducts { get; set; }
        [JsonProperty("stock")]
        public int Quantity { get; set; }
        public int _orderQuantity = 1;
        public int OrderQuantity { get => _orderQuantity; set { _orderQuantity = value; OnPropertyChanged(); TotalAmount = value * Price; } }
        public decimal _totalAmount;
        public decimal TotalAmount { get => _totalAmount > 0 ? _totalAmount : Price; set { _totalAmount = value; OnPropertyChanged(); } }
        [JsonProperty("subcategory_with_category")]
        public Subcategory Subcategory { get; set; }
        [JsonProperty("product_subcategory_id")]
        public int SubcategoryId { get; set; }
        [JsonProperty("images")]
        public List<Image> Images { get; set; }
        [JsonProperty("specification_attributes")]
        public List<AttributeValue> SpecificationAttributes { get; set; }
    }
}
