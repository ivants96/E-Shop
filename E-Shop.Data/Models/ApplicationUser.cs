using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual Person Person { get; set; }
    }
}
