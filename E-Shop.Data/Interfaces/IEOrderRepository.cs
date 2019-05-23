using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data.Interfaces
{
    public interface IEOrderRepository : IRepository<EOrder>
    {
        EOrder FindOrderIdByTokenState(int id, string token, OrderState orderState);
        
    }
}
