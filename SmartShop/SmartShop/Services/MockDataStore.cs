using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;
        readonly List<Category> categories;
        readonly List<Product> products;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };

            products = new List<Product>()
            {
                new Product { Id = 1, Name = "Item 1", Description="This is an item description.", Img="Image1", Price=100, Quantity=10 },
                new Product { Id = 1, Name = "Item 2", Description="This is an item description.", Img="Image2", Price=110, Quantity=10 },
                new Product { Id = 1, Name = "Item 3", Description="This is an item description.", Img="Image3", Price=120, Quantity=10 },
                new Product { Id = 1, Name = "Item 4", Description="This is an item description.", Img="Image4", Price=130, Quantity=10 },
                new Product { Id = 1, Name = "Item 5", Description="This is an item description.", Img="Image5", Price=140, Quantity=10 },
            };

            categories = new List<Category>()
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
                Img = "projector",
                Name = "Projectors",
            },

            new Category
            {
                Img = "server",
                Name = "Servers",
            },
        };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(categories);
        }

        public Task<IEnumerable<Product>> GetProductsAsync(bool forceRefresh = false)
        {
            return Task.FromResult<IEnumerable<Product>>(products);
        }
    }
}