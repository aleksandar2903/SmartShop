using SmartShop.ViewModels;
using System;
using System.ComponentModel;
using System.IO;
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

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var imageButton = sender as ImageButton;

            if (imageButton.Source is FileImageSource image)
            {
                string imageName = image.File;

                if (imageName == "red_heart.png")
                {
                    imageButton.Source = "heart.png";
                }
                else
                {
                    imageButton.Source = "red_heart.png";
                }
            }
        }
    }
}