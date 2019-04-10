using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Shop.Data.Models
{
    public class BankAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int BankAccoutID { get; set; }

        [StringLength(30)]
        [Display(Name = "BIC (Swift)")]
        public string Swift { get; set; }

        [StringLength(30)]
        [Display(Name = "IBAN")]
        public string Iban { get; set; }

        [StringLength(100)]
        [Display(Name = "Majiteľ účtu")]
        public string AccountOwner { get; set; }

    }
}
