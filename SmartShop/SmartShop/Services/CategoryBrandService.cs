using Newtonsoft.Json;
using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public class CategoryBrandService : ICategoryBrandService
    {
        readonly HttpClient client;

        public CategoryBrandService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Config.APIUrl)
            };
        }
        public async Task<List<Brand>> GetBrandsAsync()
        {
            var responseData = new List<Brand>();
            var response = await client.GetAsync($"brands");

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<List<Brand>>(content, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
            }

            return responseData;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var responseData = new List<Category>();
            var response = await client.GetAsync($"categories");

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<List<Category>>(content, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
            }

            return responseData;
        }

        public async Task<List<Subcategory>> GetSubcategoriesAsync(int categoryId)
        {
            var responseData = new List<Subcategory>();
            var response = await client.GetAsync($"categories/{categoryId}/subcategories");

            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                responseData = JsonConvert.DeserializeObject<List<Subcategory>>(content, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
            }

            return responseData;
        }
    }
}
