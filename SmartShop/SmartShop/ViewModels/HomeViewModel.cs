using MonkeyCache.FileStore;
using SmartShop.Models;
using SmartShop.Services;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class HomeViewModel : FavouritesViewModel
    {
        private Item _selectedItem;
        public ObservableCollection<Item> Items { get; }
        public ObservableCollection<Category> Categories { get; }
        public ObservableCollection<Product> Products { get; }
        public ObservableCollection<Product> FeaturedProducts { get; }
        public ObservableCollection<Promotion> Promotions { get; }
        public Command LoadItemsCommand { get; }
        public Command OpenCategoriesPageCommand { get; }
        public Command AddItemCommand { get; }
        public Command ProductTapped { get; }
        public Command PromotionTapped { get; }
        public Command FavouriteProduct { get; }

        public HomeViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            Categories = new ObservableCollection<Category>();
            Products = new ObservableCollection<Product>();
            FeaturedProducts = new ObservableCollection<Product>();
            Promotions = new ObservableCollection<Promotion>();
            LoadItemsCommand = new Command(async () => await LoadDataAsync());
            OpenCategoriesPageCommand = new Command(async () => await Shell.Current.Navigation.PushAsync(new ExplorePage(), true));
            PromotionTapped = new Command<Promotion>(async (promotion) => await Shell.Current.Navigation.PushAsync(new PromotionPage(promotion.Id), true));

            ProductTapped = new Command<Product>(OnProductSelected);

            var deviceInfo = DeviceDisplay.MainDisplayInfo;
            FrameSize = (int)(deviceInfo.Width / deviceInfo.Density / 1.15);
        }

        public async Task LoadDataAsync()
        {
            State = LayoutState.Loading;

            try
            {
                var categoriesTask = CategoryBrandService.GetCategoriesAsync();
                var productsTask = ProductService.GetPopularProductsAsync();
                var featuredProductsTask = ProductService.GetNewestProductsAsync();
                var promotionsTask = PromotionService.GetPromotions();

                await Task.WhenAll(featuredProductsTask, categoriesTask, productsTask, featuredProductsTask, promotionsTask);

                var featuredProducts = await featuredProductsTask;
                var products = await productsTask;
                var categories = await categoriesTask;
                var promotions = await promotionsTask;


                Categories.Clear();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }

                Products.Clear();
                var data = Barrel.Current.Get<Dictionary<int, Product>>("favs");
                foreach (var product in products)
                {
                    if (data != null && data.ContainsKey(product.Id))
                        product.Favourite = true;
                    Products.Add(product);
                }
                FeaturedProducts.Clear();
                foreach (var product in featuredProducts)
                {
                    FeaturedProducts.Add(product);
                }

                Promotions.Clear();
                foreach (var promotion in promotions)
                {
                    Promotions.Add(promotion);
                }

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                State = LayoutState.None;
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

        async void OnProductSelected(Product product)
        {

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.Navigation.PushModalAsync(new ItemDetailPage(product.Id));
        }
    }
}