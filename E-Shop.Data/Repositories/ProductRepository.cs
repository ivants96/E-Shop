using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Shop.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        public Product FindByUrl(string url)
        {
            return dbSet.FirstOrDefault(p => p.Url == url);
        }
        //returns list of products which contain current search phrase in their title, description 
        public List<Product> SearchProducts(string searchPhrase)
        {
            if (string.IsNullOrEmpty(searchPhrase))
            {
                return dbSet.Where(p => !p.Hidden).ToList();
            }
            else
            {
                return dbSet.Where(p => !p.Hidden &&
                (p.Title.Contains(searchPhrase) ||
                p.Description.Contains(searchPhrase)))
                .ToList();
            }
        }
        //returs list of products that belong to given category
        public List<Product> FindByCategoryId(int categoryId)
        {
            return dbSet.Where(p => p.CategoryProducts
            .Select(c => c.CategoryId)
            .Contains(categoryId))
            .ToList();
        }

        public Product GetPaymentInAdvance()
        {
           return dbSet.Single(p => p.Url == "prevod-na-ucet");
        }


    }
}
