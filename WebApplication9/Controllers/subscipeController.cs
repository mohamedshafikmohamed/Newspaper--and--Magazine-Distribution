using System;
using System.Collections.Generic;

using System.Web;
using Microsoft.AspNet.Identity;

using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;

using System.Web.Mvc;

using WebApplication9.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace WebApplication9.Controllers
{
    public class subscipeController : Controller
    {
        //
        // GET: /subscipe/
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult subscripe_shop()
        {
            string s=User.Identity.GetUserId();
            var user = db.Users.Find(s);
            s = user.shop_id;
            var l = db.Users.Find(s);
           
            return View(l);
        }
        public ActionResult subscripe_product()
        {
            string s=User.Identity.GetUserId();
             var l = db.subscripes.Where(x => x.customerId == s).ToList();
            List<product> products = new List<product>();
            foreach (var item in l)
            {
                products.Add(db.products.Find(item.productId));
            }
            return View(products);
        }
        public ActionResult subscripe(subscripe s)
        {

            return View();
        }
	}
}