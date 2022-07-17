using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartShop.Components.Frames
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductFrame : Grid
    {
        public Product Product { get; set; }
        public ProductFrame()
        {
            InitializeComponent();
            this.BindingContext = this;
        }
    }
}