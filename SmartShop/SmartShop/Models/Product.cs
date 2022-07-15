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
        public Image Image1 { get => _image; set { _image = value; OnPropertyChanged(); _image = value; } }
        Image _image;
        public Uri Image { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("brand")]
        public Brand Brand { get; set; }
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
