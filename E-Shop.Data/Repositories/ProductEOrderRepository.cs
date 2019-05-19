﻿using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data.Repositories
{
    public class ProductEOrderRepository : BaseRepository<ProductEOrder>, IProductEOrderRepository
    {
        public ProductEOrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}