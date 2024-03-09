﻿using FFImageLoading.Args;
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
        private int currentPage = 1;
        private int lastPage = 1;
        private bool _isInitialized;
<<<<<<< Updated upstream
=======
        private string _subcategories = string.Empty;
        private string _brand = string.Empty;
>>>>>>> Stashed changes
        private FilterRequest searchRequest = new FilterRequest();
        private FilterViewModel filterViewModel;
        private FilterPage filterPage;
        public ObservableCollection<Product> Products { get; set; }
        public Command OpenFilterPopupCommand { get; }
        public Command OnProductTapped { get; }
        public Command SearchProductsCommand { get; }
        public Command ToggleFavouriteProductCommand { get; }
        public Command LoadMoreDataCommand { get; }
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
            LoadMoreDataCommand = new Command(async () => await LoadMoreDataAsync());
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
            await LoadDataAsync();
        }

        public async void OnAppearing(string subcategories = "", string brand = "")
        {
            if (_isInitialized)
            {
                return;
            }
<<<<<<< Updated upstream
            _isInitialized = true;

            if (string.IsNullOrWhiteSpace(filterPage.Categories) && string.IsNullOrWhiteSpace(filterPage.Brand) && string.IsNullOrWhiteSpace(query))
=======

            _isInitialized = true;
            this._subcategories = subcategories;
            this._brand = brand;

            if (string.IsNullOrWhiteSpace(subcategories) && string.IsNullOrWhiteSpace(brand) && string.IsNullOrWhiteSpace(filterPage.Categories) && string.IsNullOrWhiteSpace(filterPage.Brand) && string.IsNullOrWhiteSpace(query))
>>>>>>> Stashed changes
            {
                State = LayoutState.Custom;
                CustomStateKey = StateKeys.EmptyQuery;
                return;
            }

            if (Products.Count == 0)
            {
                filterPage.Categories = subcategories;
                filterPage.Brand = brand;
                searchRequest.Brands = brand;
                searchRequest.Categories = subcategories;
                await LoadDataAsync();
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

        async Task LoadMoreDataAsync()
        {
            if (currentPage >= lastPage)
            {
                return;
            }

            currentPage++;

            IsBusy = true;

            await LoadDataAsync();

            IsBusy = false;
        }
        async Task LoadDataAsync()
        {
            if (State == LayoutState.Loading)
            {
                return;
            }

            if (!VerifyInternetConnection())
            {
                State = LayoutState.Custom;
                CustomStateKey = StateKeys.Offline;
                return;
            }

            if (!IsBusy)
            {
                State = LayoutState.Loading;
            }

            try
            {
                if (!IsBusy)
                {
                    Products.Clear();
                }

<<<<<<< Updated upstream
                var responseTask = SearchService.SearchProducts(query, searchRequest.Categories, searchRequest.Brands, searchRequest.MinPrice, searchRequest.MaxPrice, searchRequest.SortBy, page: currentPage, token: SettingsService.AuthAccessToken);
=======
                var responseTask = SearchService.SearchProducts(query, !string.IsNullOrWhiteSpace(_subcategories) ? _subcategories : searchRequest.Categories, !string.IsNullOrWhiteSpace(_brand) ? _brand : searchRequest.Brands, searchRequest.MinPrice, searchRequest.MaxPrice, searchRequest.SortBy, page: currentPage, token: SettingsService.AuthAccessToken);
>>>>>>> Stashed changes

                await Task.WhenAll(responseTask, Task.Delay(1000));

                var response = await responseTask;

                response.LastPage = response.LastPage;

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
