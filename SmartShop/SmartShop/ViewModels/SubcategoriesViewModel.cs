using SmartShop.Models;
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
    public class SubcategoriesViewModel : BaseViewModel
    {
        public ObservableCollection<Subcategory> Subcategories { get; }
        public Command SubcategoryTapped { get; }
        public SubcategoriesViewModel()
        {
            Subcategories = new ObservableCollection<Subcategory>();
            SubcategoryTapped = new Command<Subcategory>(OnSelectedSubcategory);
        }

        public async void OnInitialize(string name, int categoryId)
        {
            if(categoryId > 0)
            {
                Title = name;
                await LoadSubcategoriesAsync(categoryId);
            }
        } 

        async Task LoadSubcategoriesAsync(int categoryId)
        {
            State = LayoutState.Loading;

            try
            {
                var subcategories = await CategoryBrandService.GetSubcategoriesAsync(categoryId);

                foreach (var subcategory in subcategories)
                {
                    Subcategories.Add(subcategory);
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

        async void OnSelectedSubcategory(Subcategory subcategory)
        {
            await Shell.Current.Navigation.PushAsync(new ExplorePage(subcategory == null ? 0 : subcategory.Id));
        }
    }
}
