using Newtonsoft.Json;
using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;
        readonly List<Category> categories;
        readonly List<Product> products;
        readonly HttpClient client;

        public MockDataStore()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Config.BaseAddress)
            };
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
                new Product { Id = 1, Name = "AORUS MODEL X 12th", SubcategoryId = 4, Description="The cherry-pick Intel Core i9-12900K with factory-overclocked setting and the latest DDR5 32GB high speed RAM," +
                " which brings the stable and ultimate overclocking performance. Powered with the GeForce RTX 3080 graphics card, high efficiency chassis cooling design and liquid cooling system, " +
                "the AORUS MODEL X brings a quiet, low-temperature and high performance while delivering smooth frame rates and lifelike gaming experience.", Image = new Uri("https://www.gigabyte.com/Image/53ac1488324dceb2b9d5cd643b30e31b/Product/30311/webp/1000"), Price=2500, Quantity=10, Images = new List<Image>()
                    {
                    new Image { Id = 1, Source = new Uri("https://www.gigabyte.com/Image/53ac1488324dceb2b9d5cd643b30e31b/Product/30311/webp/1000") },
                    new Image { Id = 2, Source = new Uri("https://www.gigabyte.com/Image/872e9f9f714839c6e46387c2c43e2919/Product/30312/webp/1000") },
                    new Image { Id = 3, Source = new Uri("https://www.gigabyte.com/Image/0575c2cdca7f582ff362ac2f992b2a0a/Product/30313/webp/1000") },
                    new Image { Id = 4, Source = new Uri("https://www.gigabyte.com/Image/4c7b1af5acd20d12e975e010427ce0db/Product/30314/webp/1000") },
                    new Image { Id = 5, Source = new Uri("https://www.gigabyte.com/Image/179c9a6d6754bfdb65a4e0d7f5cdb905/Product/30315/webp/1000") },
                    }
                },

                new Product { Id = 2, Name = "AORUS M5", SubcategoryId = 1, Description="This is an item description.", Image = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/6d5f9ab9c18573ae6b74033cac9f5fdf/Product/21988/png/500"), Price=110, Quantity=10, Images = new List<Image>()
                    {
                    new Image { Id = 1, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/6d5f9ab9c18573ae6b74033cac9f5fdf/Product/21988/png/500") },
                    new Image { Id = 2, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/3087e606df6c7d7e293d636f156315d1/Product/20001/png/500") },
                    new Image { Id = 3, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/e97c709ca5969343741bfddcbf118a61/Product/19985/png/500") },
                    new Image { Id = 4, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/d5033f4602f080200fb2caa4483ff8a5/Product/19978/png/500") },
                    new Image { Id = 5, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/3f80820826d15bf6096343c6aab229a1/Product/20004/png/500") },
                    new Image { Id = 5, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/3f80820826d15bf6096343c6aab229a1/Product/20004/png/500") },
                    new Image { Id = 5, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/4d827b69bf5d903d96865ea3a4446d73/Product/20006/png/500") },
                    }},
                new Product { Id = 3, Name = "GeForce RTX™ 3080 Ti GAMING OC 12G", SubcategoryId = 7, Description="This is an item description.", Image = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/824/img/1.png"), Price=1400, Quantity=10, Images = new List<Image>()
                    {
                    new Image { Id = 1, Source = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/824/img/1.png") },
                    new Image { Id = 2, Source = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/824/img/2.png") },
                    new Image { Id = 3, Source = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/824/img/3.png") },
                    new Image { Id = 4, Source = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/824/img/4.png") },
                    new Image { Id = 5, Source = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/824/img/5.png") },
                    new Image { Id = 5, Source = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/824/img/7.png") },
                    new Image { Id = 5, Source = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/824/img/9.png") },
                    }
                },
                new Product { Id = 4, Name = "AORUS K9 Optical", Description="This is an item description.", Image = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/ab1e4624fff03736140da270889ef63a/Product/18856/png/500"), Price=130, Quantity=10, Images = new List<Image>()
                    {
                    new Image { Id = 1, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/ab1e4624fff03736140da270889ef63a/Product/18856/png/500") },
                    new Image { Id = 2, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/d90fdd72b318136b53a6b9ca472d58c1/Product/18735/png/500") },
                    new Image { Id = 3, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/d90fdd72b318136b53a6b9ca472d58c1/Product/18736/png/500") },
                    new Image { Id = 4, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/44b2d3fd8b7713f241adbd19b12b9599/Product/18737/png/500") },
                    new Image { Id = 5, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/44b2d3fd8b7713f241adbd19b12b9599/Product/18737/png/500") },
                    } },
                new Product { Id = 5, Name = "AORUS FI32Q X Gaming Monitor", Description="This is an item description.", Image = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/17b4417b7e55404cb89933887bc80d06/Product/30126/webp/1000"), Price=140, Quantity=10, Images = new List<Image>()
                    {
                    new Image { Id = 1, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/17b4417b7e55404cb89933887bc80d06/Product/30126/webp/1000") },
                    new Image { Id = 2, Source = new Uri("https://www.gigabyte.com/Image/6bf48ce9ecb239799053f11004a677b8/Product/30127/webp/1000") },
                    new Image { Id = 3, Source = new Uri("https://www.gigabyte.com/Image/b988cdfa47587c44b01c61b96694680f/Product/30128/webp/1000") },
                    new Image { Id = 4, Source = new Uri("https://www.gigabyte.com/Image/d2567fce05dff32717da8a98d26d079f/Product/30129/webp/1000") },
                    new Image { Id = 5, Source = new Uri("https://www.gigabyte.com/Image/a8c9abc451fbba2e8e6c8f98c996013c/Product/30130/webp/1000") },
                    new Image { Id = 5, Source = new Uri("https://www.gigabyte.com/Image/3386828e520c51c2c37ffbc8f250440f/Product/30133/webp/1000") },
                    }
                },

                new Product { Id = 6, Name = "Iphone 13 Pro Max", Description="This is an item description.", Image = new Uri("https://cdn.shopify.com/s/files/1/0586/6920/3620/products/iphone-13-pro-max-graphite-select_d6be573e-8cc9-4b7d-9c7a-2be0e30bbb0d_530x@2x.png?v=1631691002"), Price=1290, Quantity=10, Images = new List<Image>()
                    {
                    new Image { Id = 1, Source = new Uri("https://cdn.shopify.com/s/files/1/0586/6920/3620/products/iphone-13-pro-max-graphite-select_d6be573e-8cc9-4b7d-9c7a-2be0e30bbb0d_530x@2x.png?v=1631691002") },
                    new Image { Id = 2, Source = new Uri("https://steadfastmall.com/wp-content/uploads/2021/09/apple_iphone_13_pro_max_silver_steadfast.png") },
                    new Image { Id = 3, Source = new Uri("https://steadfastmall.com/wp-content/uploads/2021/09/apple_iphone_13_pro_max_steadfast.png") },
                    }
                },
                new Product { Id = 7, Name = "Z690 AORUS XTREME (rev. 1.0)", Description="GIGABYTE Z690 Motherboards feature the latest DDR5 architecture and XMP 3.0 capability. The new DDR5 memory technology brings 50% more bandwidth to the platform and increases the system performance drastically by implementing Unlocked Native DDR5 Voltage, Xtreme Memory Routing and reliable SMD slot. ", Image = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/c979e67da63720de783e4abe0e89ddec/Product/30105/webp/1000"), 
                    Price=650, Quantity=10, SubcategoryId = 11, Images = new List<Image>()
                    {
                    new Image { Id = 1, Source = new Uri("https://static.gigabyte.com/StaticFile/Image/Global/c979e67da63720de783e4abe0e89ddec/Product/30105/webp/1000") },
                    new Image { Id = 2, Source = new Uri("https://www.gigabyte.com/Image/6f88252e4c26bcc73279a646d74a3b96/Product/30106/webp/1000") },
                    new Image { Id = 3, Source = new Uri("https://www.gigabyte.com/Image/f487c09d3f2399c93ee7fd9964c97375/Product/30107/webp/1000") },
                    }
                },
                new Product { Id = 8, Name = "Z490 AORUS ULTRA G2 (rev. 1.x)", Description="ntel® Z490 AORUS Motherboard with Direct 12+1 Phases Digital VRM Design, Comprehensive Thermal Solution with Fins-Array II, ESSential USB DAC, Intel® WiFi 6 802.11ax, Intel® 2.5GbE LAN, RGB FUSION 2.0", 
                    Image = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/736/img/1.png"),
                    Price=450, Quantity=10, SubcategoryId = 11, Images = new List<Image>()
                    {
                    new Image { Id = 1, Source = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/736/img/1.png") },
                    new Image { Id = 2, Source = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/736/img/2.png") },
                    new Image { Id = 3, Source = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/736/img/4.png") },
                    new Image { Id = 4, Source = new Uri("https://www.gigabyte.com/FileUpload/Global/WebPage/736/img/5.png") },
                    }
                },
            };

            categories = new List<Category>()
            {
                new Category
            {
                Img = "washing_machine",
                Name = "Home appliance",
                Subcategories = new List<Subcategory>()
                {
                    new Subcategory
                    {
                        Id = 1,
                        Name  = "Washing Machines",
                    },
                    new Subcategory
                    {
                        Id = 2,
                        Name  = "Refrigerators",
                    }
                }
            },
            new Category
            {
                Img = "computer",
                Name = "PC's",
                Subcategories = new List<Subcategory>()
                {
                    new Subcategory
                    {
                        Id = 3,
                        Name  = "All-In-Ones",
                    },
                    new Subcategory
                    {
                        Id = 4,
                        Name  = "Desktops",
                    },
                    new Subcategory
                    {
                        Id = 5,
                        Name  = "Apple",
                    }
                }
            },
            new Category
            {
                Img = "computer",
                Name = "PC's Components",
                Subcategories = new List<Subcategory>()
                {
                    new Subcategory
                    {
                        Id = 6,
                        Name  = "CPU's",
                    },
                    new Subcategory
                    {
                        Id = 7,
                        Name  = "Graphics Cards",
                    },
                    new Subcategory
                    {
                        Id = 8,
                        Name  = "Memory",
                    },
                    new Subcategory
                    {
                        Id = 9,
                        Name  = "HDD's",
                    },
                    new Subcategory
                    {
                        Id = 10,
                        Name  = "SSD's",
                    },
                    new Subcategory
                    {
                        Id = 11,
                        Name  = "Motherboards",
                    }
                }
            },
            new Category
            {
                Img = "joystick",
                Name = "Gaming Consoles",
                Subcategories = new List<Subcategory>()
                {
                    new Subcategory
                    {
                        Id = 12,
                        Name  = "PlayStation Consoles",
                    },
                    new Subcategory
                    {
                        Id = 13,
                        Name  = "Xbox Consoles",
                    },
                    new Subcategory
                    {
                        Id = 14,
                        Name  = "Nintendo Consoles",
                    }
                }
            },

            new Category
            {
                Img = "projector",
                Name = "Projectors",
                Subcategories = new List<Subcategory>()
                {
                    new Subcategory
                    {
                        Id = 15,
                        Name  = "PlayStation Consoles",
                    },
                    new Subcategory
                    {
                        Id = 16,
                        Name  = "Xbox Consoles",
                    },
                    new Subcategory
                    {
                        Id = 17,
                        Name  = "Nintendo Consoles",
                    }
                }
            },

            new Category
            {
                Img = "server",
                Name = "Servers",
                Subcategories = new List<Subcategory>{
                    new Subcategory
                    {
                        Id = 18,
                        Name  = "Terminals",
                    },
                    new Subcategory
                    {
                        Id = 19,
                        Name  = "Computer Servers",
                    }
                },
            }
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

        public async Task<IEnumerable<Product>> GetProductsAsync(bool forceRefresh = false)
        {
            var response = await client.GetAsync("products/popular");

            var content = await response.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<Root<Product>>(content);
            return responseData.Results;
        }

        public async Task<IEnumerable<Product>> GetFeatureProductsAsync(bool forceRefresh = false)
        {
            var response = await client.GetAsync("products/newest");

            var content = await response.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<Root<Product>>(content);
            return responseData.Results;
        }

        public async Task<IEnumerable<Image>> GetFeatureImagesAsync(bool forceRefresh = false)
        {
            var features = new List<Image>
            {

                new Image { Id = 4, Source = new Uri("https://wallpaperaccess.com/full/2325987.jpg") },
                new Image { Id = 5, Source = new Uri("https://p4.wallpaperbetter.com/wallpaper/427/1009/251/nvidia-nvidia-rtx-gpus-graphics-card-fans-hd-wallpaper-thumb.jpg") },
            };
            return await Task.FromResult<IEnumerable<Image>>(features);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var response = await client.GetAsync($"products/{id}");

            var content = await response.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<Product>(content);
            return responseData;
        }

        public async Task<IEnumerable<Product>> GetRelatedProductsAsync(int subcategoryId, bool forceRefresh = false)
        {
            await Task.Delay(1000);
            return products.Where(s => s.SubcategoryId == subcategoryId);
        }
    }
}