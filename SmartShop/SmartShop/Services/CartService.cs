using SmartShop.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public class CartService : ICartService
    {
        private readonly string url = $"{Config.APIUrl}/carts";
        public async Task<List<Cart>> GetCarts(string token, int page = 0)
        {
            var responseData = new List<Cart>();
            

            return responseData;
        }

        public async Task<bool> ToggleProduct(string token, int productId)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("product_id", productId.ToString())
            });

            

            return false;
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
