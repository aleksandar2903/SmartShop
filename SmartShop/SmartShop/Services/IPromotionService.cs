using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public interface IPromotionService
    {
        Task<List<Promotion>>GetPromotions();
        Task<Promotion>GetPromotion(int promotionId);
    }
}
