using SmartShop.Models;
using SmartShop.Services;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        private string query;
        private FilterRequest searchRequest = new FilterRequest();
        private FilterViewModel filterViewModel;
        private FilterPage filterPage;
        public ObservableCollection<Product> Products { get; set; }
        public Command OpenFilterPopupCommand { get; }
        public Command OnProductTapped { get; }
        public Command SearchProductsCommand { get; }
        public Command ToggleFavouriteProductCommand { get; }
        public BrowseViewModel()
        {
            filterViewModel = new FilterViewModel();
            filterViewModel.FilterChanged += FilterViewModel_FilterChanged;
            filterPage = new FilterPage(filterViewModel);
            Products = new ObservableCollection<Product>();
            OpenFilterPopupCommand = new Command(OpenFilters);
            OnProductTapped = new Command<Product>(OnProductSelected);
            SearchProductsCommand = new Command<string>(async (query) => { this.query = query; await FilterProducts(); });
            ToggleFavouriteProductCommand = new Command<Product>(async (product) => await ToggleProduct(product));
        }

        private async void FilterViewModel_FilterChanged(object sender, FilterRequest e)
        {
            if (e != null)
            {
                searchRequest.Categories = e.Categories;
                searchRequest.Brands = e.Brands;
                searchRequest.MinPrice = e.MinPrice;
                searchRequest.MaxPrice = e.MaxPrice;
                searchRequest.SortBy = e.SortBy;
                await FilterProducts();
            }
        }

        void OpenFilters()
        {
            if (string.IsNullOrEmpty(filterPage.Categories) && string.IsNullOrEmpty(filterPage.Brand) && string.IsNullOrEmpty(query))
            {
                return;
            }
            filterPage.Query = query;
            Shell.Current.Navigation.PushModalAsync(filterPage);
        }

        async Task FilterProducts()
        {
            await LoadDataAsync(query, searchRequest.Categories, searchRequest.Brands, searchRequest.MinPrice, searchRequest.MaxPrice, searchRequest.SortBy);
        }

        public async void OnAppearing(string subcategories = "", string brand = "")
        {
            if (Products.Count == 0)
            {
                filterPage.Categories = subcategories;
                filterPage.Brand = brand;

                if (string.IsNullOrEmpty(filterPage.Categories) && string.IsNullOrEmpty(filterPage.Brand) && string.IsNullOrEmpty(query))
                {
                    State = LayoutState.Custom;
                    CustomStateKey = StateKeys.EmptyQuery;
                }
                else
                {
                    await LoadDataAsync(categories: subcategories, brands: brand);
                }
            }
        }

        async void OnProductSelected(Product product)
        {
            await Shell.Current.Navigation.PushModalAsync(new ItemDetailPage(product.Id));
        }

        protected override async Task RefreshData()
        {
            await LoadDataAsync();
        }

        private async Task ToggleProduct(Product product)
        {
            if (!IsLoggedIn())
            {
                await Shell.Current.GoToAsync(nameof(LoginPage), true);
                return;
            }
            if (!VerifyInternetConnection())
            {
                return;
            }

            product.Favourite = !product.Favourite;

            try
            {
                await FavouriteService.ToogleFavourite(product.Id, SettingsService.AuthAccessToken);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                product.Favourite = !product.Favourite;
            }
        }

        async Task LoadDataAsync(string query = "", string categories = "", string brands = "", decimal priceMin = 0, decimal priceMax = 0, string sortBy = "")
        {
            if (!VerifyInternetConnection())
            {
                State = LayoutState.Custom;
                CustomStateKey = StateKeys.Offline;
                return;
            }

            State = LayoutState.Loading;

            try
            {
                Products.Clear();

                var responseTask = SearchService.SearchProducts(query, categories, brands, priceMin, priceMax, sortBy, token: SettingsService.AuthAccessToken);

                await Task.WhenAll(responseTask, Task.Delay(1000));

                var response = await responseTask;

                foreach (var product in response.Results)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                State = LayoutState.Error;
            }
            finally
            {
                if (State != LayoutState.Error)
                {
                    State = Products.Count > 0 ? LayoutState.None : LayoutState.Empty;
                }
            }

        }
    }
}
