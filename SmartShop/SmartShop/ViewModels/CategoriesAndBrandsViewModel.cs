using SmartShop.Models;
using SmartShop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    public class CategoriesAndBrandsViewModel : BaseViewModel
    {
        public ObservableCollection<Category> Categories { get; }
        public Command<Category> ForwardCommand { get; }
        public CategoriesAndBrandsViewModel()
        {
            ForwardCommand = new Command<Category>(async (category) => await Shell.Current.Navigation.PushAsync(new SubcategoriesPage(category)));
            Categories = new ObservableCollection<Category>()
            {
                new Category
            {
                Img = "washing_machine",
                Name = "Washing Machines",
            },
            new Category
            {
                Img = "computer",
                Name = "PC's",
            },
            new Category
            {
                Img = "joystick",
                Name = "Gaming Consoles",
            },

            new Category
            {
                Img = "smartphone",
                Name = "Smartphones",
            },

            new Category
            {
                Img = "speaker",
                Name = "Audio Speakers",
            },
        };
        }
    }
}
