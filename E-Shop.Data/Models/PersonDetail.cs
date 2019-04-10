using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Shop.Data.Models
{
    public class PersonDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int PersonDetailId { get; set; }

        [Required(ErrorMessage = "Vyplňte meno")]
        [Display(Name = "Meno")]
        [StringLength(50, ErrorMessage = "Meno môže obsahovať maximálne 50 znakov")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vyplňte priezvisko")]
        [StringLength(50, ErrorMessage = "Priezvisko môže obsahovať maximálne 50 znakov")]
        [Display(Name = "Priezvisko")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "Spoločnosť môže obsahovať maximálne 100 znakov")]
        [Display(Name = "Spoločnosť")]
        public string CompanyName { get; set; }

        [StringLength(20, ErrorMessage = "Telefón je príliš dlhý")]
        [Display(Name = "Telefón")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vyplňte Email")]
        [StringLength(100, ErrorMessage = "Email je prílš dlhý")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Fax je prílš dlhý")]
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [StringLength(30, ErrorMessage = "DIČ je prílš dlhé")]
        [Display(Name = "DIČ")]
        public string DIČ { get; set; }

        [StringLength(20, ErrorMessage = "IČO musí mať 8 číslic")]
        [Display(Name = "IČO")]
        public string IČO { get; set; }

        [StringLength(2048)]
        [Display(Name = "Spisová značka")]
        public string RegistryEntry { get; set; } // Iba pre admina

        [NotMapped]
        public string FullName => string.Format($"{FirstName} {LastName}");
    }
}
