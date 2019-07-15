using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Vyplňte e-mailovú adresu")]
        [Display(Name = "Váš e-mail")]
        [DataType(DataType.EmailAddress)]
        public string SenderEmail { get; set; }
        [Required(ErrorMessage = "Vyplňte predmet správy")]
        [Display(Name = "Predmet")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Vyplňte text správy")]
        [Display(Name = "Text správy")]
        public string EmailBody { get; set; }
    }
}
