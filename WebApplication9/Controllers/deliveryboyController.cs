using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using WebApplication9.Models;
namespace WebApplication9.Controllers
{
    public class deliveryboyController : Controller
    {
        //
        // GET: /deliveryboy/
        private ApplicationDbContext db = new ApplicationDbContext();
         public deliveryboyController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

         public deliveryboyController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        //
        // GET: /manager/
        public ActionResult deliveryboys()
        {
            
            List<ApplicationUser> l = new List<ApplicationUser>();
            foreach (var item in db.Users)
            {
                if(UserManager.IsInRole(item.Id, "deliveryboy"))
                {
                    l.Add(item);
                }
            }

            return View(l);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult delete_orders(int id)
        {
            var x = db.orders.Find(id);
            db.orders.Remove(x);
            db.SaveChanges();
            return RedirectToAction("orders");
        }


        public ActionResult orders()
        {
             string d = User.Identity.GetUserId();
            
             
            var x = db.orders.Where(m => m.deleveryboyId.Equals(d));
 
            return View(x.ToList());
        }

        public ActionResult add_deliveryboy()
        {
            ViewBag.line = new SelectList(new[] { "cairo", "giza", "shubra", "awsem", "elma3ady" });
            return View();
        }
        
        public ActionResult delete_deliveryboy(string id)
        {
            var u=db.Users.Find(id);
            db.Users.Remove(u);
            try { db.SaveChanges(); }
            catch
            {
                ViewBag.d = "this deliveryboy havs come order must finish it before delete him";
            }
            return RedirectToAction("deliveryboys");
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> add_deliveryboy(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.line = new SelectList(new[] { "cairo", "giza", "shubra", "awsem", "elma3ady" });
                var user = new ApplicationUser() { UserName = model.UserName,phone=model.phone,line=model.line,Email=model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    await UserManager.AddToRoleAsync(user.Id, "deliveryboy");
                    return RedirectToAction("Index", "Home");
                }
               
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

      
	}
}