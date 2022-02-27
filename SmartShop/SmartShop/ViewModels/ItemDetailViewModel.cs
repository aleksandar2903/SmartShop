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
        private string photo = "Image1";
        public Command BackwardCommand { get; }

        public ItemDetailViewModel()
        {
            Photos = new List<Product>()
            {
                new Product { Id = 1, Name = "Item 1", Description="This is an item description.", Img="Image1", Price=100, Quantity=10 },
                new Product { Id = 1, Name = "Item 2", Description="This is an item description.", Img="Image2", Price=110, Quantity=10 },
                new Product { Id = 1, Name = "Item 3", Description="This is an item description.", Img="Image3", Price=120, Quantity=10 },
                new Product { Id = 1, Name = "Item 4", Description="This is an item description.", Img="Image4", Price=130, Quantity=10 },
                new Product { Id = 1, Name = "Item 5", Description="This is an item description.", Img="Image5", Price=140, Quantity=10 },
                new Product { Id = 1, Name = "Item 5", Description="This is an item description.", Img="Image5", Price=140, Quantity=10 },
                new Product { Id = 1, Name = "Item 5", Description="This is an item description.", Img="Image5", Price=140, Quantity=10 },
                new Product { Id = 1, Name = "Item 5", Description="This is an item description.", Img="Image5", Price=140, Quantity=10 },
                new Product { Id = 1, Name = "Item 5", Description="This is an item description.", Img="Image5", Price=140, Quantity=10 },
            };
            SelectedPhoto = new Command<Product>(ChangePhoto);
            BackwardCommand = new Command(async () => await Shell.Current.Navigation.PopModalAsync());
        }

        private void ChangePhoto(Product photo)
        {
            Photo = photo.Img;
        }

        public List<Product> Photos { get; }
        public Command<Product> SelectedPhoto { get; }
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

        public string Photo
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
