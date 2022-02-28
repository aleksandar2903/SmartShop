using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : Popup
    {
        public FilterPage()
        {
            InitializeComponent();
            var deviceInfo = DeviceDisplay.MainDisplayInfo;
            CustomPopup.Size = new Size(deviceInfo.Width / deviceInfo.Density, -1);
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}