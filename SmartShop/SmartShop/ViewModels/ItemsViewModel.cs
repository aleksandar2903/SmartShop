using MonkeyCache.FileStore;
using SmartShop.Models;
using SmartShop.Services;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;
        public ObservableCollection<Item> Items { get; }
        public ObservableCollection<Category> Categories { get; }
        public ObservableCollection<Product> Products { get; }
        public ObservableCollection<Product> FeaturedProducts { get; }
        public ObservableCollection<Product> OnAction { get; }
        public Command LoadItemsCommand { get; }
        public Command OpenCategoriesPageCommand { get; }
        public Command AddItemCommand { get; }
        public Command ProductTapped { get; }
        public Command FavouriteProduct { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            Categories = new ObservableCollection<Category>();
            Products = new ObservableCollection<Product>();
            FeaturedProducts = new ObservableCollection<Product>();
            OnAction = new ObservableCollection<Product>();
            LoadItemsCommand = new Command(async () => await LoadDataAsync());
            OpenCategoriesPageCommand = new Command(async () => await Shell.Current.Navigation.PushAsync(new ExplorePage(), true));

            ProductTapped = new Command<Product>(OnProductSelected);

            AddItemCommand = new Command(OnAddItem);

            var deviceInfo = DeviceDisplay.MainDisplayInfo;
            FrameSize = (int)(deviceInfo.Width / deviceInfo.Density / 1.15);
        }

        public async Task LoadDataAsync()
        {
            IsBusy = true;

            try
            {
                OnAction.Clear();
                var categoriesTask = CategoryBrandService.GetCategoriesAsync();
                var productsTask = ProductService.GetPopularProductsAsync();
                var featuredProductsTask = ProductService.GetNewestProductsAsync();

                await Task.WhenAll(featuredProductsTask, categoriesTask, productsTask, featuredProductsTask);

                var featuredProducts = await featuredProductsTask;
                var products = await productsTask;
                var categories = await categoriesTask;


                Categories.Clear();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }

                Products.Clear();
                foreach (var product in products)
                {
                    if (product.SubcategoryId == 1 || product.SubcategoryId == 4)
                        OnAction.Add(product);
                    Products.Add(product);
                }
                FeaturedProducts.Clear();
                foreach (var product in featuredProducts)
                {
                    FeaturedProducts.Add(product);
                }

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void OnAppearing()
        {
            SelectedItem = null;
            if (Products.Count == 0)
            {
                await LoadDataAsync();
            }
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }

        public int FrameSize { get; }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnProductSelected(Product product)
        {

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.Navigation.PushModalAsync(new ItemDetailPage(product.Id));
        }
    }
}