using MonkeyCache.FileStore;
using SmartShop.Services;
using SmartShop.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartShop
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = AppInfo.PackageName;
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
