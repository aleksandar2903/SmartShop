using Flurl.Http;
using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public class PromotionService : IPromotionService
    {
        public async Task<Promotion> GetPromotion(int promotionId)
        {
            var responseData = new Promotion();
            var response = await $"{Config.APIUrl}promotions/{promotionId}".AllowHttpStatus().GetAsync();

            if (response.StatusCode < 300 && response.ResponseMessage.Content != null)
            {
                responseData = await response.GetJsonAsync<Promotion>();
            }

            return responseData;
        }

        public async Task<List<Promotion>> GetPromotions()
        {
            var responseData = new List<Promotion>();
            var response = await $"{Config.APIUrl}promotions".AllowHttpStatus().GetAsync();

            if (response.StatusCode < 300 && response.ResponseMessage.Content != null)
            {
                responseData = await response.GetJsonAsync<List<Promotion>>();
            }

            return responseData;
        }
    }
}
