using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private int productId;
        private Uri photo;
        public Command BackwardCommand { get; }

        public ItemDetailViewModel(int id) : this()
        {
            productId = id;
        }
        public ItemDetailViewModel()
        {
            Products = new ObservableCollection<Product>();
            SelectedPhoto = new Command<Models.Image>(ChangePhoto);
            BackwardCommand = new Command(async () => await Shell.Current.Navigation.PopModalAsync());
        }

        private void ChangePhoto(Models.Image photo)
        {
            Photo = photo.Source;
        }

        public async void OnAppearing()
        {
            await FetchProductAsync();
        }

        public Product Product { get => _product; set => SetProperty(ref _product, value); }
        private Product _product;
        public ObservableCollection<Product> Products { get; set; }
        public Command<Models.Image> SelectedPhoto { get; }

        public Uri Photo
        {
            get => photo;
            set => SetProperty(ref photo, value);
        }

        public async Task FetchProductAsync()
        {
            IsBusy = true;

            try
            {
                var item = await DataStore.GetProductAsync(productId);
                Product = item;
                Photo = item.Image;
                var relatedProducts = await DataStore.GetRelatedProductsAsync(item.SubcategoryId);
                foreach (var product in relatedProducts)
                {
                    if (product.Id == item.Id)
                        continue;

                    Products.Add(product);
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
