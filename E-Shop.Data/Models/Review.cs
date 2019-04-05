using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Shop.Data.Models
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int ReviewId { get; set; }
        [StringLength(5000, MinimumLength = 5, ErrorMessage = "Minimálna dĺžka recenzie musí byť 5 znakov")]
        public string Content { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public DateTime Sent { get; set; }

        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
