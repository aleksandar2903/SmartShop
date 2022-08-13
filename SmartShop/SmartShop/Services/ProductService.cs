using Flurl.Http;
using Newtonsoft.Json;
using SmartShop.Models;
using SmartShop.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartShop.Services
{
    public class ProductService : IProductService
    {
        private readonly IRequestProvider _requestProvider;
        public ProductService()
        {
            _requestProvider = DependencyService.Get<IRequestProvider>();
        }
        public async Task<IEnumerable<Product>> GetPopularProductsAsync()
        {
            Root<Product> catalog = await _requestProvider.GetAsync<Root<Product>>($"{Config.APIUrl}products/popular").ConfigureAwait(false);

            return catalog?.Results ?? Enumerable.Empty<Product>();
        }

        public async Task<IEnumerable<Product>> GetNewestProductsAsync()
        {
            Root<Product> catalog = await _requestProvider.GetAsync<Root<Product>>($"{Config.APIUrl}products/newest").ConfigureAwait(false);

            return catalog?.Results ?? Enumerable.Empty<Product>();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            Product product = await _requestProvider.GetAsync<Product>($"{Config.APIUrl}products/{id}").ConfigureAwait(false);

            return product ?? new Product();
        }
    }
}