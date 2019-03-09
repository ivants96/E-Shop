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
        private ICategoryProductRepository categoryProductRepository;

        public CategoryManager(IProductRepository productRepository, ICategoryRepository categoryRepository,
            ICategoryProductRepository categoryProductRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.categoryProductRepository = categoryProductRepository;
        }

        public List<Category> GetLeaves()
        {
            return categoryRepository.GetLeaves();
        }

        public void UpdateProductCategories(int productId, int[] categories) //Find current product by it'đ Id
        {
            Product product = productRepository.FindById(productId);
            if (product == null)
            {
                throw new ArgumentNullException($"Product {productId} was not found");
            }
            // 
            var currentCategories = product.CategoryProducts.Select(x => x.CategoryId).ToList(); // stores id/ids of currently checked categories
            var removeCategories = currentCategories.Except(categories); // stores id/ids you unchecked
            var addCategories = categories.Except(currentCategories);// stores categories that have not yet been checked and can be

            // Remove current product's category/categories. CategoryId/Ids are stored in removeCategories
            foreach (var categoryId in removeCategories)
            {
                CategoryProduct toRemove = product.CategoryProducts
                                                    .Where(cp => cp.CategoryId == categoryId)
                                                    .SingleOrDefault();
                product.CategoryProducts.Remove(toRemove);
            }
            // Add new category/categories for current product. 
            foreach (var categoryId in addCategories)
            {
                CategoryProduct toAdd = new CategoryProduct()
                {
                    CategoryId = categoryId,
                    ProductId = product.ProductId
                };
                product.CategoryProducts.Add(toAdd);
                categoryProductRepository.Add(toAdd);
            }
            productRepository.Update(product);
        }



    }
}
