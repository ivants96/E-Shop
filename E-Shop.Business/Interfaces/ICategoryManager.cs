﻿using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Interfaces
{
    public interface ICategoryManager
    {
        List<Category> GetLeaves();
        void UpdateProductCategories(int productId, int[] categories);

    }
}