using Flurl.Http;
using Newtonsoft.Json;
using SmartShop.Models;
using SmartShop.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartShop.Services
{
    public class CategoryBrandService : ICategoryBrandService
    {
        private readonly IRequestProvider _requestProvider;
        public CategoryBrandService()
        {
            _requestProvider = DependencyService.Get<IRequestProvider>();
        }
        public async Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            var brands = await _requestProvider.GetAsync<IEnumerable<Brand>>($"{Config.APIUrl}brands").ConfigureAwait(false);

            return brands ?? Enumerable.Empty<Brand>();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _requestProvider.GetAsync<IEnumerable<Category>>($"{Config.APIUrl}categories").ConfigureAwait(false);

            return categories ?? Enumerable.Empty<Category>();
        }

        public async Task<IEnumerable<Subcategory>> GetSubcategoriesAsync(int categoryId)
        {
            var subcategories = await _requestProvider.GetAsync<IEnumerable<Subcategory>>($"{Config.APIUrl}categories/{categoryId}/subcategories").ConfigureAwait(false);

            return subcategories ?? Enumerable.Empty<Subcategory>();
        }
    }
}
