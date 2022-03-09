using SmartShop.Models;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class SubcategoriesViewModel : BaseViewModel
    {
        public List<Subcategory> Subcategories { get; }
        public Command SubcategoryTapped { get; }
        public SubcategoriesViewModel(Category category) : this()
        {
            Title = category?.Name;
            Subcategories = category?.Subcategories;
        }
        public SubcategoriesViewModel()
        {
            Title = "Subcategories";
            SubcategoryTapped = new Command<Subcategory>(OnSelectedSubcategory);
        }

        async void OnSelectedSubcategory(Subcategory subcategory)
        {
            await Shell.Current.Navigation.PushAsync(new ExplorePage(subcategory == null ? 0 : subcategory.Id));
        }
    }
}
