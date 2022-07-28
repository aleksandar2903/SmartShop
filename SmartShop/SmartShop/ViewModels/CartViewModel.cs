using MonkeyCache.FileStore;
using SmartShop.Models;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public Command OnProductTapped { get; }
        public CartViewModel()
        {
            Products = new ObservableCollection<Product>();
            OnProductTapped = new Command<Product>(OnProductSelected);
        }

        private void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }

        public async void OnAppearing()
        {
            await LoadFavouriteProductsAsync();
        }

        async void OnProductSelected(Product product)
        {
            await Shell.Current.Navigation.PushModalAsync(new ItemDetailPage(product.Id));
        }

        private async Task LoadFavouriteProductsAsync()
        {
            IsBusy = true;

            try
            {
                Products.Clear();

                var favProductIds = Barrel.Current.GetKeys();

                foreach (var productId in favProductIds)
                {
                    if (productId.StartsWith("c-"))
                    {
                        int id = Int32.Parse(productId.Substring(2));
                        var product = await ProductService.GetProductAsync(id);
                        Products.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
