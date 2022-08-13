using SmartShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(int id);
        Task<IEnumerable<Product>> GetPopularProductsAsync();
        Task<IEnumerable<Product>> GetNewestProductsAsync();
    }
}
