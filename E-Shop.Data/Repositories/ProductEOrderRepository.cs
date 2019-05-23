using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Shop.Data.Repositories
{
    public class ProductEOrderRepository : BaseRepository<ProductEOrder>, IProductEOrderRepository
    {
        public ProductEOrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<ProductEOrder> FindByOrderId(int orderId)
        {
            return dbSet.Where(p => p.EOrderId == orderId);
        }

        public ProductEOrder FindByOrderIdProductId(int orderId, int productId)
        {
            return dbSet.Where(p => p.EOrderId == orderId && p.ProductId == productId).SingleOrDefault();
        }
    }
}
