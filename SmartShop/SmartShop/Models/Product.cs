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
        [JsonProperty("rating_count")]
        int? ratingCount;
        public int? RatingCount { get => ratingCount > 0 ? ratingCount : null; }
        [JsonProperty("rating")]
        double rating;
        public string RatingStar
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();

                if (rating > 0)
                {
                    int ratingCell = (int)rating;
                    int remainder = 5 - ratingCell;
                    
                    if(ratingCell > 0)
                        stringBuilder.Insert(0,"★", ratingCell);

                    if (remainder > 0)
                        stringBuilder.Insert(ratingCell, "☆", remainder);

                }

                return stringBuilder.ToString();
            }
        }
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
