using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Shop.Data.Models
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int PersonId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("DeliveryAddress")]
        public int DeliveryAddressId { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public int PersonDetailId { get; set; }

        public int? BankAccountId { get; set; }

        public virtual Address Address { get; set; }

        public virtual Address DeliveryAddress { get; set; }

        public virtual PersonDetail PersonDetail { get; set; }

        public virtual BankAccount BankAccount { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
