using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; }

        public ProfileViewModel()
        {
            LogoutCommand = new Command(() => SettingsService.AuthAccessToken = String.Empty);
        }
    }
}
