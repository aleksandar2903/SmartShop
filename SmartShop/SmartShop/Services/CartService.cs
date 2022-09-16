using SmartShop.Models;
using SmartShop.Services.RequestProvider;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartShop.Services
{
    public class CartService : ICartService
    {
        private readonly string url = $"{Config.APIUrl}/cart";
        private readonly IRequestProvider _requestProvider;
        public CartService()
        {
            _requestProvider = DependencyService.Get<IRequestProvider>();
        }

        public async Task<IEnumerable<Cart>> GetCartProductsAsync(string token)
        {
            var catalog = await _requestProvider.GetAsync<IEnumerable<Cart>>(url, token).ConfigureAwait(false);

            return catalog ?? Enumerable.Empty<Cart>();
        }

        public async Task ToggleProductAsync(int productId, string token)
        {
            var data = new
            {
                product_id = productId,
            };
            await _requestProvider.PostAsync<object, object>(url, data, token).ConfigureAwait(false);
        }

        public async Task<bool> UpdateQuantity(string token, int id, int quantity)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("quantity", quantity.ToString())
            });


            return false;
        }
    }
}
