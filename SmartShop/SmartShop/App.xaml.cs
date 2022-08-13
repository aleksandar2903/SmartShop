using Flurl.Http;
using Flurl.Http.Configuration;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using SmartShop.Services;
using SmartShop.Services.RequestProvider;
using SmartShop.Services.Settings;
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
            FlurlHttp.Configure(settings => {
                var jsonSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                };
                settings.JsonSerializer = new NewtonsoftJsonSerializer(jsonSettings);
            });
            Barrel.ApplicationId = AppInfo.PackageName;
            DependencyService.Register<ProductService>();
            DependencyService.Register<CategoryBrandService>();
            DependencyService.Register<SearchService>();
            DependencyService.Register<CartService>();
            DependencyService.Register<PromotionService>();
            DependencyService.Register<SettingsService>();
            DependencyService.Register<RequestProvider>();
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
