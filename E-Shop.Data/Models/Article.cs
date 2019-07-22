using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Shop.Data.Models
{
    public class Article
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vyplňte titul článku")]
        [Display(Name = "Titul")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Vyplňte obsah článku")]
        [Display(Name = "Obsah")]
        public string Content { get; set; }
        [Required]
        [Display(Name = "Typ článku")]
        public ArticleType ArticleType { get; set; }
    }

    public enum ArticleType
    {
        JobOffer,
        Info
    }
}
