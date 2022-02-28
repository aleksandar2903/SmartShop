using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartShop.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private Uri photo;
        public Command BackwardCommand { get; }

        public ItemDetailViewModel(Product product, List<Product> products) : this()
        {
            Product = product;
            Photo = product.Images[0].Source;
            Products = products;
        }
        public ItemDetailViewModel()
        {
            Products = new List<Product>();
            SelectedPhoto = new Command<Models.Image>(ChangePhoto);
            BackwardCommand = new Command(async () => await Shell.Current.Navigation.PopModalAsync());
        }

        private void ChangePhoto(Models.Image photo)
        {
            Photo = photo.Source;
        }

        public Product Product { get; }
        public List<Product> Products { get; }
        public Command<Models.Image> SelectedPhoto { get; }
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public Uri Photo
        {
            get => photo;
            set => SetProperty(ref photo, value);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
