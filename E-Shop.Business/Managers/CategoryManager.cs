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
    public class CategoryManager : ICategoryManager
    {
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;

        public CategoryManager(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public List<Category> GetLeaves()
        {
            return categoryRepository.GetLeaves();
        }

        public void UpdateProductCategories(int productId, int[] categories)
        {
            var product = productRepository.FindById(productId);
            if(product == null)
            {
                throw new ArgumentNullException($"Product {productId} was not found");
            }

            var currentCategories = product.CategoryProducts.Select(cp => cp.CategoryId).ToList();
            var removeCategories = currentCategories.Except(categories).ToList();
            var addCategories = categories.Except(currentCategories).ToList();  

            foreach(var categoryId in removeCategories)
            {
                var toRemove = product.CategoryProducts
                    .Where(cp => cp.CategoryId == categoryId)
                    .SingleOrDefault();
                product.CategoryProducts.Remove(toRemove);
            }

            foreach(var categoryId in addCategories)
            {
                var toAdd = new CategoryProduct()
                {
                    CategoryId = categoryId,
                    ProductId = product.ProductId
                };
                product.CategoryProducts.Add(toAdd);
            }
            productRepository.Update(product);
        }
    }
}
