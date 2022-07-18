using SmartShop.Models;
using SmartShop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        private decimal minPrice;
        private decimal maxPrice;
        private decimal minPriceValidation;
        private decimal maxPriceValidation;
        private int totalRecords;
        private string query;
        private ISearchService SearchService { get; }

        public ICommand OnCategorySelectCommand { get; }
        public ICommand OnPriceChangedCommand { get; }
        public ICommand OnBrandSelectCommand { get; }
        public ICommand OnResetTappedCommand { get; }
        public ObservableCollection<Brand> Brands { get; set; }
        public ObservableCollection<Subcategory> Categories { get; set; }
        public int TotalRecords { get => totalRecords; set { SetProperty(ref totalRecords, value); } }
        public decimal MinPrice
        {
            get => minPrice;
            set => SetProperty(ref minPrice, value >= minPriceValidation ? value : minPriceValidation);
        }
        public decimal MaxPrice
        {
            get => maxPrice;
            set => SetProperty(ref maxPrice, value <= maxPriceValidation ? value : maxPriceValidation);
        }
        public Dictionary<int, int> SelectedCategories { get; set; }
        public Dictionary<int, int> SelectedBrands { get; set; }

        public FilterViewModel()
        {
            Brands = new ObservableCollection<Brand>();
            Categories = new ObservableCollection<Subcategory>();
            SelectedBrands = new Dictionary<int, int>();
            SelectedCategories = new Dictionary<int, int>();
            SearchService = new SearchService();
            OnBrandSelectCommand = new Command<int>(OnBrandSelect);
            OnCategorySelectCommand = new Command<int>(OnCategorySelect);
            OnResetTappedCommand = new Command(ResetFilters);
            OnPriceChangedCommand = new Command(async () =>
            {
                if (MinPrice > minPriceValidation && MaxPrice < maxPriceValidation && MinPrice <= MaxPrice)
                    await FilterProducts();
            });
        }

        async void ResetFilters()
        {
            SelectedBrands.Clear();
            SelectedCategories.Clear();
            SetPrice();

            await FilterProducts();
        }

        void SetPrice(decimal minPrice = 0, decimal maxPrice = 0)
        {
            MaxPrice = maxPriceValidation = maxPrice;
            MinPrice = minPriceValidation = minPrice;
        }

        public async void OnInitialize(string query = "")
        {
            this.query = query;
            await FilterProducts();
        }

        async void OnCategorySelect(int categoryId)
        {
            if (SelectedCategories.ContainsKey(categoryId))
                SelectedCategories.Remove(categoryId);
            else
                SelectedCategories.Add(categoryId, categoryId);

            SetPrice();

            await FilterProducts();
        }

        async void OnBrandSelect(int brandId)
        {
            if (SelectedBrands.ContainsKey(brandId))
                SelectedBrands.Remove(brandId);
            else
                SelectedBrands.Add(brandId, brandId);

            SetPrice();

            await FilterProducts();
        }

        async Task FilterProducts()
        {
            IsBusy = true;

            try
            {
                string categories = String.Join(",", SelectedCategories.Keys);
                string brands = String.Join(",", SelectedBrands.Keys);
                var response = await SearchService.SearchProducts(query, categories, brands, MinPrice, MaxPrice);
                Categories.Clear();
                Brands.Clear();
                SetPrice(response.MinProductPrice, response.MaxProductPrice);
                foreach (var category in response.Categories)
                {
                    category.IsActive = SelectedCategories.ContainsKey(category.Id);
                    Categories.Add(category);
                }

                foreach (var brand in response.Brands)
                {
                    brand.IsActive = SelectedBrands.ContainsKey(brand.Id);
                    Brands.Add(brand);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
