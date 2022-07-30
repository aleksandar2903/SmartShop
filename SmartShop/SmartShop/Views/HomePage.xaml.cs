using SmartShop.ViewModels;
using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartShop.Views
{
    public partial class HomePage : ContentPage
    {
        public HomeViewModel viewModel;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new HomeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}