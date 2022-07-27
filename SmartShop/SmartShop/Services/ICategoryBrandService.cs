using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public interface ICategoryBrandService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Subcategory>> GetSubcategoriesAsync(int categoryId);
        Task<List<Brand>> GetBrandsAsync();
    }
}
