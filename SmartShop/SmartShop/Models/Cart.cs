using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartShop.Models
{
    public class Cart : INotifyPropertyChanged
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
        int _quantity = 1;
        [JsonProperty("quantity")]
        public int Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(); OnPropertyChanged(nameof(Amount)); } }
        public Product Product { get; set; }
        public decimal Amount => Quantity * Product.Price;
    }
}