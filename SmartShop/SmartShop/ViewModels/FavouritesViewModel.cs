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
    public class FavouritesViewModel : BaseViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public ICommand RemoveFavouriteProduct { get; }
        public ICommand OnProductTapped { get; }
        public FavouritesViewModel()
        {
            Products = new ObservableCollection<Product>();
            RemoveFavouriteProduct = new Command<Product>(RemoveProduct);
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

        private async Task LoadFavouriteProductsAsync()
        {
            IsBusy = true;

            try
            {
                Products.Clear();

                var favProductIds = Barrel.Current.GetKeys();

                foreach (var productId in favProductIds)
                {
                    if (productId.StartsWith("f-"))
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

        async void OnProductSelected(Product product)
        {
            await Shell.Current.Navigation.PushModalAsync(new ItemDetailPage(product.Id));
        }
    }
}
