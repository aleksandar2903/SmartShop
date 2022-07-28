using Flurl.Http;
using Newtonsoft.Json;
using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public class ProductService : IProductService
    {
        public async Task<List<Product>> GetPopularProductsAsync()
        {
            var responseData = new Root<Product>();
            var response = await $"{Config.APIUrl}products/popular".AllowHttpStatus().GetAsync();

            if (response.StatusCode < 300 && response.ResponseMessage.Content != null)
            {
                responseData = await response.GetJsonAsync<Root<Product>>();
            }

            return responseData.Results;
        }

        public async Task<List<Product>> GetNewestProductsAsync()
        {
            var responseData = new Root<Product>();
            var response = await $"{Config.APIUrl}products/newest".AllowHttpStatus().GetAsync();

            if (response.StatusCode < 300 && response.ResponseMessage.Content != null)
            {
                responseData = await response.GetJsonAsync<Root<Product>>();
            }

            return responseData.Results;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var responseData = new Product();
            var response = await $"{Config.APIUrl}products/{id}".AllowHttpStatus().GetAsync();

            if (response.StatusCode < 300 && response.ResponseMessage.Content != null)
            {
                responseData = await response.GetJsonAsync<Product>();
            }

            return responseData;
        }
    }
}