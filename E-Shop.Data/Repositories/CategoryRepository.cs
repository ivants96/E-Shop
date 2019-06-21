using E_Shop.Data.Interfaces;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Shop.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }


        public List<Category> GetLeaves(bool includeHidden = false)
        {
            if (includeHidden)
            {
                return dbSet.Where(c => c.ChildCategories.Count == 0).ToList();
            }
            else
            {
                return dbSet.Where(c => c.ChildCategories.Count == 0 && !c.Hidden).ToList();
            }
        }

        public List<Category> GetRoots()
        {
            return dbSet.Where(c => c.ParentCategoryId == null && !c.Hidden)
                .OrderBy(c => c.OrderNo)
                .ToList();
        }

        public Category GetTransportCategory()
        {
            return dbSet.Single(c => c.Url == "sposoby-dopravy");
        }

        public Category GetWayOfPaymentCategory()
        {
            return dbSet.Single(c => c.Url == "sposoby-platby");
        }
    }
}
