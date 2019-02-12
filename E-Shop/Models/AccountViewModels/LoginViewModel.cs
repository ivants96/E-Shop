using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.AccountViewModels
{
    public class LoginViewModel
{
        [Required(ErrorMessage = "Pre prihlásenie musíte zadať e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pre prihlásenie musíte zadať e-mail heslo")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Zapamätaj si ma")]
        public bool RememberMe { get; set; }
    }
}
