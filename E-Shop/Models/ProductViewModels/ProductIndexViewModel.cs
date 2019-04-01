using E_Shop.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace E_Shop.Models.ProductViewModels
{
    public class ProductIndexViewModel
    {
        //Currently displayed product by current category and info needed for paging
        public IPagedList<Product> Products { get; set; }

        public decimal? StartPrice { get; set; }
        public decimal? EndPrice { get; set; }

        public bool InStock { get; set; }

        public string SortCriteria { get; set; }
        // info about current category and search product by name in search form
        public int? CurrentCategoryId { get; set; }
        public string CurrentPhrase { get; set; }

        //Dropdown Sortlist
        public List<SelectListItem> SortList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(){ Text= "Hodnotenie", Value = "rating"},
            new SelectListItem(){ Text= "Najnižšia cena", Value = "lowest_price"},
            new SelectListItem(){ Text= "Najvyšia cena", Value = "highest_price"},
            new SelectListItem(){ Text= "Najnovšie", Value = "newest"}
        };
    }
}
