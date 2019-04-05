using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        IEnumerable<Review> FindByProductId(int productId);
        Review FindByUserIdProductId(string userId, int productId);
    }
}
