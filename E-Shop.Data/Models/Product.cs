using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Shop.Data.Models
{
    public class Product
    {
        public Product()
        {
            ImagesCount = 0;
            Hidden = false;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Vyplňte kód ")]
        [StringLength(255, ErrorMessage = "Kód je príliš dlhý")]
        [Display(Name = "Kód produktu")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Vyplňte url ")]
        [StringLength(255, ErrorMessage = "Url je príliš dlhá")]
        [RegularExpression(@"^[a-zA-Z0-9\-]+$", ErrorMessage = "Používajte len písmená bez diakritiky alebo číslice")]
        [Display(Name = "Url")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Vyplňte titulok ")]
        [StringLength(255, ErrorMessage = "Titulok je príliš dlhý")]
        [Display(Name = "Titulok")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vyplňte krátký popis ")]
        [StringLength(255, ErrorMessage = "Krátký popis je príliš dlhý")]
        [Display(Name = "Krátký popis")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Vyplňte popis ")]
        [Display(Name = "Popis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Vyplňte cenu")]
        [Display(Name = "Cena")]
        [Range(0, 1000000000000, ErrorMessage = "Cena nesmie byť záporná")]
        public decimal Price { get; set; }

        [Display(Name = "Cena pred zľavou")]
        [Range(1, 1000000000000, ErrorMessage = "Cena pred zľavou nesmie byť záporná")]
        public decimal? OldPrice { get; set; }

        [Required(ErrorMessage = "Vyplňte počet kusov na sklade")]
        [Display(Name = "Skladom")]
        [Range(0, int.MaxValue, ErrorMessage = "Počet kusov na sklade nesmie byť záporný")]
        public int Stock { get; set; }

        [Display(Name = "Obrázkov produktu celkom")]
        [Range(0, int.MaxValue, ErrorMessage = "Počet obrázkov nesmie byť záporný")]
        public int ImagesCount { get; set; }

        [Display(Name = "Skryť")]
        public bool Hidden { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }


    }
}
