using E_Shop.Business.Interfaces;
using E_Shop.Data.Models;
using E_Shop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Managers
{
    public class ProductManager : IProductManager
    {
        private IProductRepository productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
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
            var oldProduct = productRepository.FindById(product.ProductId);
            productRepository.Add(product);

            if (oldProduct != null)
            {
                CleanProduct(oldProduct);
            }
        }

        public void CleanProduct(Product oldProduct)
        {
            try
            {
                productRepository.Delete(oldProduct.ProductId);
            }
            catch (Exception)
            {
                oldProduct.CategoryProducts.Clear();    // odstraníme produkt z kategorií
                oldProduct.Hidden = true;               // a skryjeme ho
                productRepository.Update(oldProduct);
            }
        }

        
    }
}
