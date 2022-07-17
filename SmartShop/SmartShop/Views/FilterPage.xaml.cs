using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartShop.Views
{
    public partial class FilterPage : Popup
    {
        public SearchResponse Response { get; set; }
        public ObservableCollection<Brand> Brands { get; set; }
        public ObservableCollection<Subcategory> Categories { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<int> SelectedCategories { get; set; }
        public List<int> SelectedBrands { get; set; }
        public FilterPage()
        {
            InitializeComponent();
            var deviceInfo = DeviceDisplay.MainDisplayInfo;
            CustomPopup.Size = new Size(deviceInfo.Width / deviceInfo.Density, -1);
            Categories = new ObservableCollection<Subcategory>();
            Brands = new ObservableCollection<Brand>();
            SelectedCategories = new List<int>();
            SelectedBrands = new List<int>();
            this.BindingContext = this;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            this.Dismiss(false);
        }

        private void ApplyFilters_Clicked(object sender, EventArgs e)
        {
            this.Dismiss(true);
        }

        private void TapCategoryGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var checkedGrid = sender as Grid;

            foreach (var child in checkedGrid.Children)
            {
                if(child is Label label)
                    label.FontAttributes = label.FontAttributes == FontAttributes.Bold ? FontAttributes.None : FontAttributes.Bold;
                else if(child is CheckBox checkBox)
                    checkBox.IsChecked = !checkBox.IsChecked;
            }

            if (checkedGrid.BindingContext != null && checkedGrid.BindingContext is Subcategory category)
            {
                if (SelectedCategories.Contains(category.Id))
                    SelectedCategories.Remove(category.Id);
                else
                    SelectedCategories.Add(category.Id);
            }
        }

        private void TapBrandGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var checkedGrid = sender as Grid;

            foreach (var child in checkedGrid.Children)
            {
                if (child is Label label)
                    label.FontAttributes = label.FontAttributes == FontAttributes.Bold ? FontAttributes.None : FontAttributes.Bold;
                else if (child is CheckBox checkBox)
                    checkBox.IsChecked = !checkBox.IsChecked;
            }

            if (checkedGrid.BindingContext != null && checkedGrid.BindingContext is Brand brand)
            {
                if(SelectedBrands.Contains(brand.Id))
                    SelectedBrands.Remove(brand.Id);
                else
                    SelectedBrands.Add(brand.Id);
            }
        }

        private void MinPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Decimal.TryParse(((Entry)sender).Text, out decimal minPrice))
                MinPrice = minPrice;
        }

        private void MaxPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Decimal.TryParse(((Entry)sender).Text, out decimal maxPrice))
                MaxPrice = maxPrice;
        }
    }
}