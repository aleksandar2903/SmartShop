﻿using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public interface ICartService
    {
        Task ToggleProductAsync(int productId, string token);
        Task<bool> UpdateQuantity(string token, int id, int quantity);
        Task<List<Cart>> GetCarts(string token, int page = 0);
    }
}
