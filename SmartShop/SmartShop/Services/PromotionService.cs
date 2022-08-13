using SmartShop.Models;
using SmartShop.Services.RequestProvider;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartShop.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IRequestProvider _requestProvider;
        public PromotionService()
        {
            _requestProvider = DependencyService.Get<IRequestProvider>();
        }
        public async Task<Promotion> GetPromotion(int promotionId)
        {
            var promotion = await _requestProvider.GetAsync<Promotion>($"{Config.APIUrl}promotions/{promotionId}").ConfigureAwait(false);

            return promotion ?? new Promotion();
        }

        public async Task<IEnumerable<Promotion>> GetPromotions()
        {
            var promotions = await _requestProvider.GetAsync<IEnumerable<Promotion>>($"{Config.APIUrl}promotions").ConfigureAwait(false);

            return promotions ?? Enumerable.Empty<Promotion>();
        }
    }
}
