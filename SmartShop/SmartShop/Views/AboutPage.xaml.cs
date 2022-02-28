using SmartShop.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartShop.Views
{
    public partial class AboutPage : ContentPage
    {
        public ItemsViewModel viewModel;
        public int FrameSize { get; } 
        public AboutPage()
        {
            InitializeComponent();
            var deviceInfo = DeviceDisplay.MainDisplayInfo;
            FrameSize = (int)(deviceInfo.Width / deviceInfo.Density);
            BindingContext = viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}