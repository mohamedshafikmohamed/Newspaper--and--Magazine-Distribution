using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
namespace WebApplication9.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string line { get; set; }
        public string phone { get; set; }
        public string Delivery_address { get; set; }
        public virtual ICollection< product>products { get; set; }

        public string Name { get; set; }
        public string Email { get; set; } 
        public string shop_id { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<WebApplication9.Models.product> products { get; set; }

       
        public System.Data.Entity.DbSet<WebApplication9.Models.orders> orders { get; set; }

        public System.Data.Entity.DbSet<WebApplication9.Models.subscripe> subscripes { get; set; }


       
     
    


      
    }
}