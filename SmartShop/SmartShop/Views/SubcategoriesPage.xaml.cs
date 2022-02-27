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
    public partial class SubcategoriesPage : ContentPage
    {
        SubcategoriesViewModel viewModel;
        public SubcategoriesPage(Category category)
        {
            InitializeComponent();
            BindingContext = viewModel = new SubcategoriesViewModel(category);
        }
    }
}