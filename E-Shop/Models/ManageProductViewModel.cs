using E_Shop.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class ManageProductViewModel
    {
        public ManageProductViewModel()
        {
            Product = new Product();
            AvailableCategories = new List<Category>();
            PostedCategories = new bool[0];
            UploadedImages = new List<IFormFile>();
        }

        public Product Product { get; set; }

        private List<Category> _availableCategories;
        public List<Category> AvailableCategories
        {
            get
            {
                return _availableCategories;
            }
            set
            {
                PostedCategories = new bool[value.Count];
                _availableCategories = value;
            }
        }

        [Required(ErrorMessage = "Musíte vybrať najmenej jednu kategóriu")]
        [Display(Name = "Kategorie")]
        public bool[] PostedCategories { get; set; }

        public List<IFormFile> UploadedImages { get; set; }

        public string FormCaption { get; set; }

    }
}
