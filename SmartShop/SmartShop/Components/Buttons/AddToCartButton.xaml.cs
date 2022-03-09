﻿using MonkeyCache.FileStore;
using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartShop.Components.Buttons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddToCartButton : Button
    {
        public AddToCartButton()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (this.BindingContext == null) return;

            if (this.BindingContext is Product product && product.Id >= 0)
            {
                if (Barrel.Current.Exists($"c-{product.Id}"))
                {
                    button.Text = "Add To Cart";
                    Barrel.Current.Empty($"c-{product.Id}");
                    if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        await Shell.Current.DisplayToastAsync("Removed from cart", 3000);
                    }
                }
                else
                {
                    button.Text = "Added To Cart";
                    Barrel.Current.Add($"c-{product.Id}", product.Id, TimeSpan.FromDays(90));
                    if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        await Shell.Current.DisplayToastAsync("Added to cart", 3000);
                    }
                }

            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (this.BindingContext == null) return;

            if (this.BindingContext is Product product && product.Id >= 0)
            {
                if (Barrel.Current.Exists($"c-{product.Id}"))
                {
                    this.Text = "Added To Cart";
                }
                else
                {
                    this.Text = "Add To Cart";
                }

            }
        }
    }
}