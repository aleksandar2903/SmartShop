using SmartShop.Models;
using SmartShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExplorePage : ContentPage
    {
        BrowseViewModel viewModel;
        //int subcategoryId;
        public ExplorePage(int subcategoryId = 0)
        {
            InitializeComponent();
            BindingContext = viewModel = new BrowseViewModel();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    viewModel.OnAppearing();
        //}
    }
}