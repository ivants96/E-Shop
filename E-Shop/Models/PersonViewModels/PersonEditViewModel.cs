using AutoMapper;
using E_Shop.Classes;
using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.PersonViewModels
{
    public class PersonEditViewModel
    {
        
        public PersonEditViewModel()
        {

        }

        [Required(ErrorMessage = "Email je povinný")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RequiredIfEmpty("CompanyName", ErrorMessage = "Vyplňte meno")]
        [Display(Name = "Meno")]
        [StringLength(50, ErrorMessage = "Meno môže obsahovať maximálne 50 znakov")]
        public string FirstName { get; set; }

        [RequiredIfNotEmpty("FirstName", ErrorMessage = "Vyplňte priezvisko")]
        [StringLength(50, ErrorMessage = "Priezvisko môže obsahovať maximálne 50 znakov")]
        [Display(Name = "Priezvisko")]
        public string LastName { get; set; }

        [RequiredIfEmpty("FirstName", ErrorMessage = "Vyplňte názov spoločnosťi")]
        [StringLength(100, ErrorMessage = "Spoločnosť môže obsahovať maximálne 100 znakov")]
        [Display(Name = "Spoločnosť")]
        public string CompanyName { get; set; }

        [RequiredIfNotEmpty("CompanyName", ErrorMessage = "Vyplňte fax")]
        [StringLength(20, ErrorMessage = "Fax je prílš dlhý")]
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [RequiredIfNotEmpty("CompanyName", ErrorMessage = "Vyplňte DIČ")]
        [StringLength(30, ErrorMessage = "DIČ je prílš dlhé")]
        [Display(Name = "DIČ")]
        public string DIČ { get; set; }

        [RequiredIfNotEmpty("CompanyName", ErrorMessage = "Vyplňte IČO")]
        [StringLength(20, ErrorMessage = "IČO musí mať 8 číslic")]
        [Display(Name = "IČO")]
        public string IČO { get; set; }

        [Required(ErrorMessage = "Zadajte tel. číslo")]
        [StringLength(20, ErrorMessage = "Telefón je príliš dlhý")]
        [Display(Name = "Telefón")]
        public string PhoneNumber { get; set; }

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
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Vyberte krajinu")]
        [Display(Name = "Štát")]
        public Country Country { get; set; }

        //Delivery Addresss
                
        [StringLength(50)]
        [Display(Name = "Ulica a číslo domu")]
        public string StreetHouseNumberDelivery { get; set; }
      
        [StringLength(100)]
        [Display(Name = "Mesto/Obec")]
        public string CityDelivery { get; set; }
       
        [StringLength(30, ErrorMessage = "PSČ je príliš dlhé")]
        [Display(Name = "PSČ")]
        public string PostalCodeDelivery { get; set; }
               
        [Display(Name = "Štát")]
        public Country CountryDelivery { get; set; }    

    }
}
