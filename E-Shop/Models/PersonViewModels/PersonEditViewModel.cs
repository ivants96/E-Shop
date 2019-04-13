using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using E_Shop.Data.Models;

namespace E_Shop.Models.PersonViewModels
{
    public class PersonEditViewModel : BasePersonViewModel
    {
        public PersonEditViewModel(Person person) : base(person)
        {
        }

        [StringLength(30)]
        [Display(Name = "BIC (Swift)")]
        public string Swift { get; set; }

        [StringLength(30)]
        [Display(Name = "IBAN")]
        public string Iban { get; set; }

        [StringLength(100)]
        [Display(Name = "Majiteľ účtu")]
        public string AccountOwner { get; set; }

        [StringLength(2048)]
        [Display(Name = "Spisová značka")]
        public string RegistryEntry { get; set; } // Iba pre admina
    }
}
