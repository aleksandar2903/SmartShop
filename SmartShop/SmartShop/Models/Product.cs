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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Uri Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int _orderQuantity = 1;
        public int OrderQuantity { get => _orderQuantity; set { _orderQuantity = value; OnPropertyChanged(); TotalAmount = value * Price; } }
        public decimal _totalAmount;
        public decimal TotalAmount { get => _totalAmount > 0 ? _totalAmount : Price; set { _totalAmount = value; OnPropertyChanged(); } }
        public Subcategory Subcategory { get; set; }
        public int SubcategoryId { get; set; }
        public List<Image> Images { get; set; }
    }
}
