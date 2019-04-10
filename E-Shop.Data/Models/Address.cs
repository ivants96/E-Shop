
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Shop.Data.Models
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Vyplňte názov ulice a číslo domu")]
        [StringLength(50)]
        [Display(Name = "Ulica a číslo domu")]
        public string StreetNameAndHouseNumber { get; set; }

        [Required(ErrorMessage = "Vyplňte názov mesta/obce")]
        [StringLength(100)]
        [Display(Name = "Mesto/Obec")]
        public string City { get; set; }

        [Required(ErrorMessage = "Vyplňte PSČ")]
        [StringLength(30, ErrorMessage = "PSČ je príliš dlhé")]
        [Display(Name = "PSČ")]
        public int PostalCode { get; set; }

        [Required(ErrorMessage = "Vyberte krajinu")]
        public Country Country { get; set; }

        


    }
}
