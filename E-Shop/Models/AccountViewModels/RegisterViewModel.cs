using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email je povinný")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Heslo je povinné")]
        [StringLength(100, ErrorMessage = "{0} musí obsahovať aspoň {2} a najviac {1} znaků", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdenie hesla")]
        [Compare("Password", ErrorMessage = "Zadaná heslá sa nezhodujú")]
        public string ConfirmPassword { get; set; }
    }
}
