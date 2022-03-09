﻿using SmartShop.Models;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class BrowseViewModel : BaseViewModel
    {
        private int subcategoryId;
        public ObservableCollection<Product> Products { get; set; }
        public Command OpenFilterPopupCommand { get; }
        public Command BackwardCommand { get; }
        public Command OnProductTapped { get; }
        public BrowseViewModel(int subcategoryId)
        {
            this.subcategoryId = subcategoryId;
            Products = new ObservableCollection<Product>();
            OpenFilterPopupCommand = new Command(async () => await App.Current.MainPage.Navigation.ShowPopupAsync(new FilterPage()));
            BackwardCommand = new Command(async () => await Shell.Current.Navigation.PopAsync());
            OnProductTapped = new Command<Product>(OnProductSelected);
        }

        public async void OnAppearing()
        {
            await LoadProducts();
        }

        async void OnProductSelected(Product product)
        {
            await Shell.Current.Navigation.PushModalAsync(new ItemDetailPage(product.Id));
        }

        async Task LoadProducts()
        {
            IsBusy = true;

            try
            {
                Products.Clear();

                var products = await DataStore.GetRelatedProductsAsync(subcategoryId);

                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
