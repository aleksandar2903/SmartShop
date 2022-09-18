using SmartShop.Services.Settings;
using SmartShop.ViewModels;
using SmartShop.Views;
using Xamarin.Forms;

namespace SmartShop
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(ShippingPage), typeof(ShippingPage));
            Routing.RegisterRoute(nameof(CheckoutPage), typeof(CheckoutPage));
        }
    }
}
