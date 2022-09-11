using MonkeyCache.FileStore;
using SmartShop.Services;
using SmartShop.Services.Settings;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartShop
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = AppInfo.PackageName;
            DependencyService.Register<ProductService>();
            DependencyService.Register<CategoryBrandService>();
            DependencyService.Register<SearchService>();
            DependencyService.Register<CartService>();
            DependencyService.Register<PromotionService>();
            DependencyService.Register<SettingsService>();
            DependencyService.Register<IFavouriteService, FavouriteService>();
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
