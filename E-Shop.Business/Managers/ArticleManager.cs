using E_Shop.Business.Interfaces;
using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Managers
{
    public class ArticleManager : IArticleManager
    {
        private IArticleRepository articleRepository;

        public ArticleManager(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public void CreateArticle(Article article)
        {
            articleRepository.Add(article);
        }

        public void DeleteArticle(int id)
        {
            articleRepository.Delete(id);
        }

        public void Edit(Article article)
        {
            articleRepository.Update(article);
        }

        public IEnumerable<Article> GetInfoArticles() => articleRepository.GetInfoArticles();
        public IEnumerable<Article> GetJobOfferArticles() => articleRepository.GetJobOfferArticles();

        public Article GetArticle(int id)
        {
            return articleRepository.FindById(id);
        }
    }
}
