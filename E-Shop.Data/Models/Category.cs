using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Shop.Data.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vyplňte url")]
        [StringLength(255, ErrorMessage = "Url je príliš dlhá")]
        [Display(Name = "Url")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Vyplňte titulok ")]
        [StringLength(255, ErrorMessage = "Titulok je príliš dlhý")]
        [Display(Name = "Titulok")]
        public string Title { get; set; }

        [Required]
        public int OrderNo { get; set; }

        [Required]
        [Display(Name = "Skryť")]
        public bool Hidden { get; set; }

        public int? ParentCategoryId { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }

        [ForeignKey("ParentCategoryId")]
        [InverseProperty("ChildCategories")]
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Category> ChildCategories { get; set; }
    }
}
