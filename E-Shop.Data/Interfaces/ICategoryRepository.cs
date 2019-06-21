using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        List<Category> GetLeaves(bool includeHidden = false);
        List<Category> GetRoots();
        Category GetTransportCategory();
        Category GetWayOfPaymentCategory();
    }
}
