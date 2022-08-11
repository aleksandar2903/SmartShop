using Flurl.Http;
using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public class CartService : ICartService
    {
        public async Task<List<Cart>> GetCarts(string token, int page = 0)
        {
            var responseData = new List<Cart>();
            var response = await $"{Config.APIUrl}carts".AllowHttpStatus().GetAsync();

            if (response.StatusCode < 300 && response.ResponseMessage.Content != null)
            {
                responseData = await response.GetJsonAsync<List<Cart>>();
            }

            return responseData;
        }

        public async Task<bool> ToggleProduct(string token, int productId)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("product_id", productId.ToString())
            });

            var response = await $"{Config.APIUrl}carts".WithOAuthBearerToken(token).AllowHttpStatus().PostAsync(content);

            if (response.StatusCode < 300)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateQuantity(string token, int id, int quantity)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("quantity", quantity.ToString())
            });

            var response = await $"{Config.APIUrl}carts/{id}".WithOAuthBearerToken(token).AllowHttpStatus().PatchAsync(content);

            if (response.StatusCode < 300)
            {
                return true;
            }

            return false;
        }
    }
}
