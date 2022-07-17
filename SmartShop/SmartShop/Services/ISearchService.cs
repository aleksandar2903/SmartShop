﻿using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.Services
{
    public interface ISearchService
    {
        Task<SearchResponse> SearchProducts(string query = "", string categories = "", string brands = "", decimal priceMin = 0, decimal priceMax = 0, string sortBy = "", int page = 1);
    }
}
