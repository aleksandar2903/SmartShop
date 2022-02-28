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

        public ExplorePage(Subcategory subcategory) : this()
        {
            BindingContext = new BrowseViewModel(subcategory);
        }

        public ExplorePage()
        {
            InitializeComponent();
            BindingContext = new BrowseViewModel();
        }
    }
}