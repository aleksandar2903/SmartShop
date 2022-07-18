using SmartShop.Models;
using SmartShop.Services;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        private int subcategoryId;
        private string query;
        private FilterPage filterPage;
        private SearchResponse response;
        public ObservableCollection<Product> Products { get; set; }
        public Command OpenFilterPopupCommand { get; }
        public Command BackwardCommand { get; }
        public Command OnProductTapped { get; }
        public Command SearchProductsCommand { get; }
        public BrowseViewModel()
        {
            filterPage = new FilterPage(new FilterViewModel());
            filterPage.Dismissed += async (s, e) => { if (e.Result is bool appliedFiltes && appliedFiltes) await FilterProducts();  };
            Products = new ObservableCollection<Product>();
            OpenFilterPopupCommand = new Command(OpenFilters);
            BackwardCommand = new Command(async () => await Shell.Current.Navigation.PopAsync());
            OnProductTapped = new Command<Product>(OnProductSelected);
            SearchProductsCommand = new Command<string>(async (query) => { this.query = query; await LoadProducts(query); });
        }

        void OpenFilters()
        {
            Shell.Current.Navigation.ShowPopup(filterPage);
        }

        async Task FilterProducts()
        {
            //string brands = String.Join(",", filterPage.SelectedBrands);
            //string categories = String.Join(",", filterPage.SelectedCategories);
            //await LoadProducts(query, categories, brands, filterPage.MinPrice, filterPage.MaxPrice);
        }

        public async void OnAppearing()
        {
            await LoadProducts();
            //foreach (var category in response.Categories)
            //{
            //    filterPage.Categories.Add(category);
            //}

            //foreach (var brand in response.Brands)
            //{
            //    filterPage.Brands.Add(brand);
            //}
        }

        async void OnProductSelected(Product product)
        {
            await Shell.Current.Navigation.PushModalAsync(new ItemDetailPage(product.Id));
        }

        async Task LoadProducts(string query = "", string categories = "", string brands = "", decimal priceMin = 0, decimal priceMax = 0)
        {
            IsBusy = true;

            try
            {
                Products.Clear();

                ISearchService searchService = new SearchService();

                response = await searchService.SearchProducts(query, categories, brands, priceMin, priceMax);

                foreach (var product in response.Products.Results)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
