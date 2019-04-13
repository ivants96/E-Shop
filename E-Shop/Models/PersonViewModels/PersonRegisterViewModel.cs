using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using E_Shop.Data.Models;

namespace E_Shop.Models.PersonViewModels
{
    public class PersonRegisterViewModel : BasePersonViewModel
    {
        public PersonRegisterViewModel(Person person) : base(person)
        {
        }

        [Required(ErrorMessage = "Heslo je povinné")]
        [StringLength(100, ErrorMessage = "{0} musí obsahovať aspoň {2} a najviac {1} znakov", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potvrďte heslo")]
        [StringLength(100, ErrorMessage = "{0} musí obsahovať aspoň {2} a najviac {1} znakov", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrdenie hesla")]
        [Compare("Password", ErrorMessage = "Zadaná heslá sa nezhodujú")]
        public string ConfirmPassword { get; set; }
    }
}
