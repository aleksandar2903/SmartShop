using Newtonsoft.Json;
using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace SmartShop.Services
{
    public class SearchService : ISearchService
    {
        public async Task<Root<Product>> SearchProducts(string query, string categories = "", string brands = "", decimal priceMin = 0, decimal priceMax = 0, string sortBy = "", int page = 1, int mainCategory = 0)
        {
            var responseData = new Root<Product>();
            var response = await $"{Config.APIUrl}{(mainCategory > 0 ? $"category/{mainCategory}/" : "")}search?page={page}&query={query}&categories={categories}&brands={brands}&priceMin={priceMin}&priceMax={priceMax}&sortBy={sortBy}".AllowHttpStatus().GetAsync();

            if (response.StatusCode < 300 && response.ResponseMessage.Content != null)
            {
                responseData = await response.GetJsonAsync<Root<Product>>();
            }

            return responseData;
        }

        public async Task<FilterResponse> FilterProducts(string query, string categories = "", string brands = "", decimal priceMin = 0, decimal priceMax = 0, int mainCategory = 0)
        {
            var responseData = new FilterResponse();
            var response = await $"{Config.APIUrl}{(mainCategory > 0 ? $"category/{mainCategory}/" : "")}filter?query={query}&categories={categories}&brands={brands}&priceMin={priceMin}&priceMax={priceMax}".AllowHttpStatus().GetAsync();

            if (response.StatusCode < 300 && response.ResponseMessage.Content != null)
            {
                responseData = await response.GetJsonAsync<FilterResponse>();
            }

            return responseData;
        }
    }
}
