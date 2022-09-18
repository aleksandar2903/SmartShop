﻿using MonkeyCache.FileStore;
using SmartShop.Models;
using SmartShop.Models.Request;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(async () => await Shell.Current.Navigation.PushAsync(new RegisterPage()));
        }

        private async void OnLoginClicked(object obj)
        {
            if (State == LayoutState.Loading)
            {
                return;
            }

            if (!VerifyInternetConnection())
            {
                State = LayoutState.Custom;
                CustomStateKey = StateKeys.Offline;
                return;
            }

            State = LayoutState.Loading;

            try
            {
                var request = new LoginRequest
                {
                    Email = Email,
                    Password = Password,
                };

                var response = await AuthService.LogIn(request);

                if (response != null)
                {
                    SettingsService.AuthAccessToken = response.Token;
                    var cart = await CartService.GetCartAsync(SettingsService.AuthAccessToken);
                    var producstInCart = Barrel.Current.GetKeys();
                    var tasks = new List<Task>();
                    foreach (var productId in producstInCart)
                    {
                        if (Int32.TryParse(productId, out int id))
                        {
                            int quantity = Barrel.Current.Get<int>(productId);
                            var current = cart.Where(c => c.Product.Id == id && c.Quantity != quantity).FirstOrDefault();
                            if (current is null)
                            {
                                tasks.Add(CartService.ToggleProductInCartAsync(id, SettingsService.AuthAccessToken, quantity));
                            }
                            else
                            {
                                await CartService.UpdateQuantity(current.Id, quantity, SettingsService.AuthAccessToken);
                            }
                        }
                    }
                    var task = Task.WhenAll(tasks);
                    await task;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                State = LayoutState.None;
                if (!IsLoggedIn())
                {
                    await Shell.Current.DisplayAlert("Greška", "Uneli ste neispravne podatke. Pokusajte ponovo", "U redu");
                }
                else
                {
                    await Shell.Current.Navigation.PopModalAsync();
                }
            }
        }
    }
}
