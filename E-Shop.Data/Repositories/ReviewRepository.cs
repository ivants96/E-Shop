using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Shop.Data.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context) { }

        public IEnumerable<Review> FindByProductId(int productId)
        {
            return dbSet.Where(r => r.ProductId == productId).ToList();
        }

        public Review FindByUserIdProductId(string userId, int productId)
        {
            return dbSet.Where(r => r.UserId == userId && r.ProductId == productId).SingleOrDefault();
        }
    }
}
