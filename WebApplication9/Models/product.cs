using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class product
    {

        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int stock { get; set; }

        public string supplierId { get; set; }
        public  virtual ApplicationUser supplier { get; set; }
    }
}