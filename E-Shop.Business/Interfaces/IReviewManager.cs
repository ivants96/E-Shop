using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Interfaces
{
    public interface IReviewManager
    {
        IEnumerable<Review> GetReviews(int productId);
        void AddReview(Review review);
    }
}
