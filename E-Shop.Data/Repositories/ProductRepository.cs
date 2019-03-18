using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Shop.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }
        
        public Product FindByUrl(string url)
        {
            return dbSet.SingleOrDefault(p => p.Url == url && !p.Hidden);
        }

        
    }
}
