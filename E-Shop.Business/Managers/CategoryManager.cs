using E_Shop.Business.Interfaces;
using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using E_Shop.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
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
        private IMemoryCache memoryCache;

        public CategoryManager(IProductRepository productRepository, ICategoryRepository categoryRepository,
            ICategoryProductRepository categoryProductRepository, IMemoryCache memoryCache)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.categoryProductRepository = categoryProductRepository;
            this.memoryCache = memoryCache;
        }

        public List<Category> GetLeaves()
        {
            return categoryRepository.GetLeaves();
        }

        public List<Category> GetRoots()
        {
            if(!memoryCache.TryGetValue("CategoryRoots", out List<Category> result))
            {
                result = categoryRepository.GetRoots();
                memoryCache.Set("CategoryRoots", result, new DateTimeOffset(DateTime.Now.AddHours(1)));
            }
            return result;
        }

        public void UpdateProductCategories(int productId, int[] categories) //Enter product id and array of categories you wish to add
        {
            Product product = productRepository.FindById(productId);

            if (product == null)
            {
                throw new ArgumentNullException($"Product {productId} was not found");
            }

            for (int i = 0; i < categories.Length; i++)
            {
                CategoryProduct toAdd = new CategoryProduct()
                {
                    CategoryId = categories[i],
                    ProductId = product.ProductId
                };
                product.CategoryProducts.Add(toAdd);
                categoryProductRepository.Update(toAdd);

            }
        }





    }
}
