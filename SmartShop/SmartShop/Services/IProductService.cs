using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(int id);
        Task<List<Product>> GetPopularProductsAsync();
        Task<List<Product>> GetNewestProductsAsync();
    }
}
