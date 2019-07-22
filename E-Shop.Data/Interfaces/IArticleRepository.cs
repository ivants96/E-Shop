using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        IEnumerable<Article> GetInfoArticles();
        IEnumerable<Article> GetJobOfferArticles();
    }
}
