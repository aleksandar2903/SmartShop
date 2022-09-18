﻿using SmartShop.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();
        }
    }
}