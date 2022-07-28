using Flurl.Http;
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
        public async Task<List<Brand>> GetBrandsAsync()
        {
            var responseData = new List<Brand>();
            var response = await $"{Config.APIUrl}brands".AllowHttpStatus().GetAsync();

            if (response.StatusCode < 300 && response.ResponseMessage.Content != null)
            {
                responseData = await response.GetJsonAsync<List<Brand>>();
            }

            return responseData;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var responseData = new List<Category>();
            var response = await $"{Config.APIUrl}categories".AllowHttpStatus().GetAsync();

            if (response.StatusCode < 300 && response.ResponseMessage.Content != null)
            {
                responseData = await response.GetJsonAsync<List<Category>>();
            }

            return responseData;
        }

        public async Task<List<Subcategory>> GetSubcategoriesAsync(int categoryId)
        {
            var responseData = new List<Subcategory>();
            var response = await $"{Config.APIUrl}categories/{categoryId}/subcategories".AllowHttpStatus().GetAsync();

            if (response.StatusCode < 300 && response.ResponseMessage.Content != null)
            {
                responseData = await response.GetJsonAsync<List<Subcategory>>();
            }

            return responseData;
        }
    }
}
