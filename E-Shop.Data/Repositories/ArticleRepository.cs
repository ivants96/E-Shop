using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Shop.Data.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Article> GetInfoArticles()
        {
            return dbSet.Where(a => a.ArticleType == ArticleType.Info).ToList();
        }

        public IEnumerable<Article> GetJobOfferArticles()
        {
            return dbSet.Where(a => a.ArticleType == ArticleType.JobOffer).ToList();
        }
    }
}
