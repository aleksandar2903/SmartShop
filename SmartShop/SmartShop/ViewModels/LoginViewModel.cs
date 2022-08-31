using SmartShop.Models.Request;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var request = new LoginRequest
                {
                    Email = Email,
                    Password = Password,
                };

                var response = await AuthService.LogIn(request);

                if(response != null)
                {
                    SettingsService.AuthAccessToken = response.Token;
                    await Shell.Current.Navigation.PopModalAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
