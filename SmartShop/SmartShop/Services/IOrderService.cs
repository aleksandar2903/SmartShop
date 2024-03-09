using SmartShop.Models;
using SmartShop.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public interface IOrderService
    {
<<<<<<< Updated upstream
        Task AddOrderAsync(OrderRequest request, string token);
=======
        Task<string> AddOrderAsync(OrderRequest request, string token);
>>>>>>> Stashed changes
        Task<IEnumerable<Order>> GetOrdersAsync(string token);
    }
}
