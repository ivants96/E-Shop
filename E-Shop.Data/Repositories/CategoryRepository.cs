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
        public List<Category> GetLeaves()
        {
            return dbSet.Where(c => c.ChildCategories.Count == 0 && !c.Hidden).ToList();            
        }
    }
}
