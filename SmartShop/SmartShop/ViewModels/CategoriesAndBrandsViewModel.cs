using SmartShop.Models;
using SmartShop.Services;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class CategoriesAndBrandsViewModel : BaseViewModel
    {
        public ObservableCollection<Category> Categories { get; }
        public ObservableCollection<Brand> Brands { get; }
        public Command<Category> ForwardCommand { get; }
        public CategoriesAndBrandsViewModel()
        {
            Categories = new ObservableCollection<Category>();
            Brands = new ObservableCollection<Brand>();
            ForwardCommand = new Command<Category>(async (category) => await Shell.Current.Navigation.PushAsync(new SubcategoriesPage(category)));
        }

        public async void OnAppearing()
        {
            if(Categories.Count == 0 || Brands.Count == 0)
                await LoadDataAsync();
        }

        async Task LoadDataAsync()
        {
            State = LayoutState.Loading;

            try
            {
                Categories.Clear();
                Brands.Clear();

                var categories = await CategoryBrandService.GetCategoriesAsync();

                foreach (var category in categories)
                {
                    Categories.Add(category);
                }

                var brands = await CategoryBrandService.GetBrandsAsync();

                foreach (var brand in brands)
                {
                    Brands.Add(brand);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                State = LayoutState.None;
            }
        }
    }
}
