using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.ViewModels
{
    public class SubcategoriesViewModel : BaseViewModel
    {
        private Category _category;
        public List<Subcategory> Subcategories { get; }
        public SubcategoriesViewModel(Category category) : this()
        {
            Title = category.Name;
            _category = category;
        }
        public SubcategoriesViewModel()
        {
            Title = "Subcategories";
            Subcategories = new List<Subcategory>()
            {
                new Subcategory
            {
                Img = "Icon_Mens_Shoe",
                Name = "Men",
            },
            new Subcategory
            {
                Img = "women_shoe",
                Name = "Women",
            },
            new Subcategory
            {
                Img = "devices",
                Name = "Devices",
            },

            new Subcategory
            {
                Img = "headphone",
                Name = "Gadgets",
            },

            new Subcategory
            {
                Img = "Icon_Gaming",
                Name = "Gaming",
            },
        };
        }
    }
}
