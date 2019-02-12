using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.AccountViewModels
{
    public class ChangePasswordViewModel
{
        [Required(ErrorMessage = "Pole Aktuálne heslo je povinné.")]
        [DataType(DataType.Password)]
        [Display(Name = "Aktuálne heslo")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Pole Nové heslo je povinné.")]
        [StringLength(100, ErrorMessage = "Heslo musí byť aspoň 6 znakov dlhé.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nové heslo")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdenie nového hesla")]
        [Compare("NewPassword", ErrorMessage = "Zadané heslá sa nezhodujú.")]
        public string ConfirmPassword { get; set; }
    }
}
