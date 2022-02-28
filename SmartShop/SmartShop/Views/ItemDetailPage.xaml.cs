using SmartShop.Models;
using SmartShop.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace SmartShop.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage(Product product, List<Product> products)
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel(product, products);
        }
    }
}