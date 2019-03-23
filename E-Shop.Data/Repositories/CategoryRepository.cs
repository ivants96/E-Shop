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
        public CategoryRepository(ApplicationDbContext context) : base (context) { }

        public List<Category> GetLeaves()
        {
            // Gets categories that are last in a tree of categories e.g It gets Dynamic from tree rMusicEquipment-Microphones-Dynamic
            return dbSet.Where(c => c.ChildCategories.Count == 0 && !c.Hidden).ToList();            
        }

        public List<Category> GetRoots()
        {
            return dbSet.Where(c => c.ParentCategoryId == null && !c.Hidden)
                .OrderBy(c => c.OrderNo)
                .ToList();
        }
    }
}
