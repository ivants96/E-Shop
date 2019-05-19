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
    }
}
