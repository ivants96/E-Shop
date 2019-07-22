using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Interfaces
{
    public interface IArticleManager
    {
        IEnumerable<Article> GetInfoArticles();
        IEnumerable<Article> GetJobOfferArticles();
        Article GetArticle(int id);
        void CreateArticle(Article article);
        void DeleteArticle(int id);
        void Edit(Article article);

    }
}
