using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;
using System.IO;
using System.Web.Helpers;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace WebApplication9.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var x = db.Users;
            return View(x.ToList());
        }
        public ActionResult search()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult search(string search)
        {
             int n;
            try {  n = int.Parse(search); }
            catch
            {
                n = -1;
            }
            var result = db.products.Where(x => x.name.Contains(search)||x.description.Contains(search));
            return View(result.ToList());
        }
        [HttpPost]
        public ActionResult subscripe_shop_p(string id)
        {
            string s2 = User.Identity.GetUserId();
           
               var u=db.Users.Find(s2);
        
            if(u==null)
            {
                return RedirectToAction("Index");
            }
            u.shop_id = id;
            db.Entry(u).State = EntityState.Modified;
               
                db.SaveChanges();

                return RedirectToAction("subscripe_shop", "subscipe");
        }

        [HttpPost]
        public ActionResult subscripe(int id)
        {
            string s2 = User.Identity.GetUserId();
            var x = db.subscripes;
            bool found=false;
            foreach (var item in x.ToList())
            {
                if(item.productId==id&&item.customerId.Equals(s2))
                {
                    found = true;
                    break;
                }
            }
           
            if (found== false)
            {
                subscripe s = new subscripe();
                s.productId = id;

                s.customerId = User.Identity.GetUserId();
                db.subscripes.Add(s);
                db.SaveChanges();
            }
            return RedirectToAction("subscripe_product", "subscipe");
        }
        public ActionResult Details(int? id)
        {
            Session["productid"] = id;
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}