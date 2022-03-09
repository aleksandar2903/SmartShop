using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<Product> GetProductAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync(bool forceRefresh = false);
        Task<IEnumerable<Product>> GetProductsAsync(bool forceRefresh = false);
        Task<IEnumerable<Product>> GetFeatureProductsAsync(bool forceRefresh = false);
        Task<IEnumerable<Product>> GetRelatedProductsAsync(int subcategoryId, bool forceRefresh = false);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<Image>> GetFeatureImagesAsync(bool forceRefresh = false);
    }
}
