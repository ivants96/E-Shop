using E_Shop.Business.Interfaces;
using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using E_Shop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Shop.Business.Managers
{
    public class ProductManager : IProductManager
    {
        private IProductRepository productRepository;
        private ICategoryProductRepository categoryProductRepository;

        public ProductManager(IProductRepository productRepository, ICategoryProductRepository categoryProductRepository)
        {
            this.productRepository = productRepository;
            this.categoryProductRepository = categoryProductRepository;
        }

        public Product FindProductById(int id)
        {
            return productRepository.FindById(id);
        }

        public Product FindProductByUrl(string url)
        {
            return productRepository.FindByUrl(url);
        }

        public void SaveProduct(Product product)
        {               
            productRepository.Update(product);                       
        }

        public void CleanProduct(int id)
        {
            var product = FindProductById(id);
            product.CategoryProducts.Clear();
            productRepository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            var product = FindProductById(id);
            product.CategoryProducts.Clear();
            productRepository.Delete(id);
        }


    }
}
