using SmartShop.Models;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        public List<Product> Products { get; set; }
        public Command OpenFilterPopupCommand { get; }
        public Command BackwardCommand { get; }
        public BrowseViewModel(Subcategory subcategory) : this()
        {
            Products = subcategory.Products;
        }
        public BrowseViewModel()
        {
            Products = new List<Product>();
            OpenFilterPopupCommand = new Command(async () => await Open());
            BackwardCommand = new Command(async () => await Shell.Current.Navigation.PopAsync());
        }

        async Task Open()
        {
            var result = await App.Current.MainPage.Navigation.ShowPopupAsync(new FilterPage());
        }
    }
}
