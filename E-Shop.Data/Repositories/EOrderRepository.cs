using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data.Repositories
{
    public class EOrderRepository : BaseRepository<EOrder>, IEOrderRepository
    {
        public EOrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public EOrder FindOrderIdByTokenState(int id, string token, OrderState orderState)
        {
            EOrder result = FindById(id);
            if (result == null || result.Token != token || result.OrderState != orderState)
            {
                return null;
            }
            return result;
        }
    }
}
