using E_Shop.Business.Interfaces;
using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Managers
{
    public class ReviewManager : IReviewManager
    {
        IReviewRepository reviewRepository;

        public ReviewManager(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public void AddReview(Review review)
        {
            if (reviewRepository.FindByUserIdProductId(review.UserId, review.ProductId) == null) 
            {
                review.Sent = DateTime.Now;
                reviewRepository.Add(review);
            }
            else
            {
                throw new Exception("Tento produkt si už hodnotil");
            }
        }

        public IEnumerable<Review> GetReviews(int productId)
        {
            return reviewRepository.FindByProductId(productId);
        }
    }
}
