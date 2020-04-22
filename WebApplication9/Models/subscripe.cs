using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class subscripe
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public string customerId { get; set; }
        public virtual product product { get; set; }
        public virtual ApplicationUser customer { get; set; }
    }
}