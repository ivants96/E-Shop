using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data.Interfaces
{
    public interface IProductEOrderRepository : IRepository<ProductEOrder>
    {
        ProductEOrder FindByOrderIdProductId(int orderId, int productId);
        IEnumerable<ProductEOrder> FindByOrderId(int orderId);

    }
}
