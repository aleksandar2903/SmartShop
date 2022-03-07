using MonkeyCache.FileStore;
using SmartShop.Models;
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
        public ObservableCollection<Models.Image> Features { get; }
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
            Features = new ObservableCollection<Models.Image>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadCategoriesCommand());
            OpenCategoriesPageCommand = new Command(async () => await Shell.Current.Navigation.PushAsync(new ExplorePage(), true));

            ProductTapped = new Command<Product>(OnProductSelected);
            FavouriteProduct = new Command<Product>(OnProductFavourited);

            AddItemCommand = new Command(OnAddItem);

            var deviceInfo = DeviceDisplay.MainDisplayInfo;
            FrameSize = (int)(deviceInfo.Width / deviceInfo.Density / 1.15);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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

        public async Task ExecuteLoadCategoriesCommand()
        {
            IsBusy = true;

            try
            {
                Features.Clear();
                var features = await DataStore.GetFeatureImagesAsync(true);
                foreach (var feature in features)
                {
                    Features.Add(feature);
                }

                Categories.Clear();
                var categories = await DataStore.GetCategoriesAsync(true);
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }

                Products.Clear();
                var prodcts = await DataStore.GetProductsAsync(true);
                foreach (var product in prodcts)
                {
                    Products.Add(product);
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
            IsBusy = true;
            SelectedItem = null;
            await ExecuteLoadCategoriesCommand();
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

        void OnProductFavourited(Product product)
        {
            if(product is null || product.Id == 0)
            {
                return;
            }

            if (Barrel.Current.Exists($"f-{product.Id}"))
            {
                Barrel.Current.Empty($"f-{product.Id}");
            }
            else
            {
                Barrel.Current.Add($"f-{product.Id}", product.Id, TimeSpan.FromDays(90));
            }
        }
    }
}