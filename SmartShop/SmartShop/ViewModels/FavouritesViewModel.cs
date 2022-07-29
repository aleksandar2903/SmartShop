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
        Dictionary<int, Product> _favorites;
        public ObservableCollection<Product> FavouriteProducts { get; set; }
        public ICommand ToggleFavouriteProductCommand { get; }
        public ICommand OnProductTapped { get; }
        public int id;
        public FavouritesViewModel()
        {
            FavouriteProducts = new ObservableCollection<Product>();
            ToggleFavouriteProductCommand = new Command<Product>(ToggleProduct);
            OnProductTapped = new Command<Product>(OnProductSelected);
        }

        private void ToggleProduct(Product product)
        {
            if(_favorites == null)
                _favorites = Barrel.Current.Get<Dictionary<int, Product>>("favs") ?? new Dictionary<int, Product>();

            if (_favorites.ContainsKey(product.Id))
                _favorites.Remove(product.Id);
            else
                _favorites.Add(product.Id, product);

            Barrel.Current.Empty("favs");
            Barrel.Current.Add("favs", _favorites, TimeSpan.FromDays(90));
            product.Favourite = !product.Favourite;
        }

        public async void OnAppearing()
        {
            await LoadFavouriteProductsAsync();
        }

        private async Task LoadFavouriteProductsAsync()
        {
            IsBusy = true;
            await Task.Delay(600);
            try
            {
                FavouriteProducts.Clear();

                var favProducts = Barrel.Current.Get<Dictionary<int, Product>>("favs");

                foreach (var product in favProducts.Values)
                {
                    FavouriteProducts.Add(product);
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
