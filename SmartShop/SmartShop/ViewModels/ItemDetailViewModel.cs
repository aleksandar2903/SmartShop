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
        public Command BackwardCommand { get; }

        public ItemDetailViewModel(int id) : this()
        {
            productId = id;
        }
        public ItemDetailViewModel()
        {
            Products = new ObservableCollection<Product>();
            SelectedPhoto = new Command<Models.Image>(ChangePhoto);
            SwipeRightCommand = new Command<Models.Image>((image) => SwipePhoto(image, true));
            SwipeLeftCommand = new Command<Models.Image>((image) => SwipePhoto(image, false));
            BackwardCommand = new Command(async () => await Shell.Current.Navigation.PopModalAsync());
        }

        void SwipePhoto(Models.Image photo, bool directionRight)
        {
            if (Product.Images.Count <= 1)
                return;

            int currentPhotoIndex = Product.Images.FindIndex(s => s.Id == photo.Id);

            int nextPhotoIndex = directionRight ? currentPhotoIndex - 1 : currentPhotoIndex + 1;

            if (nextPhotoIndex < 0)
                nextPhotoIndex = Product.Images.Count - 1;

            if (nextPhotoIndex >= Product.Images.Count)
                nextPhotoIndex = 0;

            var nextPhoto = Product.Images[nextPhotoIndex];

            ChangePhoto(nextPhoto);
        }

        private void ChangePhoto(Models.Image photo)
        {
            Product.Image1 = photo;
        }

        public async void OnAppearing()
        {
            await FetchProductAsync();
        }

        public Product Product { get => _product; set => SetProperty(ref _product, value); }
        private Product _product;
        public ObservableCollection<Product> Products { get; set; }
        public Command<Models.Image> SelectedPhoto { get; }
        public Command<Models.Image> SwipeRightCommand { get; }
        public Command<Models.Image> SwipeLeftCommand { get; }

        public async Task FetchProductAsync()
        {
            IsBusy = true;

            try
            {
                var item = await DataStore.GetProductAsync(productId);
                Product = item;
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
