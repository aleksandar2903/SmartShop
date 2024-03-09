using SmartShop.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartShop.Views
{
<<<<<<< Updated upstream
=======
    [QueryProperty("disableLoad", "disabledLoad")]
>>>>>>> Stashed changes
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel viewModel;
<<<<<<< Updated upstream
=======
        public bool? disableLoad { set; get; } = false;
>>>>>>> Stashed changes
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ProfileViewModel();
        }

        protected override async void OnAppearing()
        {
<<<<<<< Updated upstream
            await viewModel.InitializeAsync();
=======
            if (!(bool)disableLoad)
            {
                await viewModel.InitializeAsync();
            }
>>>>>>> Stashed changes
        }
    }
}