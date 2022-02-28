using SmartShop.Models;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class CategoriesAndBrandsViewModel : BaseViewModel
    {
        public ObservableCollection<Category> Categories { get; }
        public Command<Category> ForwardCommand { get; }
        public CategoriesAndBrandsViewModel()
        {
            Categories = new ObservableCollection<Category>();
            ForwardCommand = new Command<Category>(async (category) => await Shell.Current.Navigation.PushAsync(new SubcategoriesPage(category)));
        }

        public async void OnAppearing()
        {
            await GetCategories();
        }

        async Task GetCategories()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Categories.Clear();

                var categories = await DataStore.GetCategoriesAsync();

                foreach (var category in categories)
                {
                    Categories.Add(category);
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
