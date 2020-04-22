using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class orders
    {

        public int id { get; set; }
        public int productId { get; set; }

        public string deleveryboyId { get; set; }
        public string custId { get; set; }

        public virtual ApplicationUser deleveryboy { get; set; }
        public virtual product product { get; set; }
    }
}