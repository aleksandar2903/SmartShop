using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;
using MonkeyCache.FileStore;
using Xamarin.Essentials;

namespace SmartShop.Components.Buttons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouriteButton : ImageButton
    {
        public int Id { get; set; }
        public FavouriteButton()
        {
            InitializeComponent();
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var imageButton = sender as ImageButton;

            if (this.BindingContext == null) return;

            string content = this.BindingContext.ToString();

            if (Int32.TryParse(content, out int id) && id >= 0)
            {
                if (Barrel.Current.Exists($"f-{id}"))
                {
                    imageButton.Source = "heart.png";
                    Barrel.Current.Empty($"f-{id}");
                    if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        await Shell.Current.DisplayToastAsync("Removed from favourites", 3000);
                    }
                }
                else
                {
                    imageButton.Source = "red_heart.png";
                    Barrel.Current.Add($"f-{id}", id, TimeSpan.FromDays(90));
                    if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        await Shell.Current.DisplayToastAsync("Added to favourites", 3000);
                    }
                }

            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (this.BindingContext == null) return;

            string content = this.BindingContext.ToString();

            if (Int32.TryParse(content, out int id) && id >= 0)
            {
                if (Barrel.Current.Exists($"f-{id}"))
                {
                    this.Source = "red_heart.png";
                }
                else
                {
                    this.Source = "heart.png";
                }

            }
        }
    }
}