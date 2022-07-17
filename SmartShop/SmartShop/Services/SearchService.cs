using Newtonsoft.Json;
using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public class SearchService : ISearchService
    {
        readonly HttpClient client;

        public SearchService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Config.BaseAddress)
            };
        }

        public async Task<SearchResponse> SearchProducts(string query, string categories = "", string brands = "", decimal priceMin = 0, decimal priceMax = 0, string sortBy = "", int page = 1)
        {
            var response = await client.GetAsync($"search?page={page}&query={query}&categories={categories}&brands={brands}&priceMin={priceMin}&priceMax={priceMax}&sortBy={sortBy}");

            var content = await response.Content.ReadAsStringAsync();

            var responseData = JsonConvert.DeserializeObject<SearchResponse>(content);
            return responseData;
        }
    }
}
