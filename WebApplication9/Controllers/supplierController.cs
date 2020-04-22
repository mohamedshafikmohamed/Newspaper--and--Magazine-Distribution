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
    [Authorize(Roles = "admin")]
    public class supplierController : Controller
    { private ApplicationDbContext db = new ApplicationDbContext();
      

        public supplierController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public supplierController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        //
        // GET: /manager/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ViewBag.line = new SelectList(new[] { "cairo", "giza", "shubra", "awsem", "elma3ady" });
                var user = new ApplicationUser() { UserName = model.UserName, phone = model.phone };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    await UserManager.AddToRoleAsync(user.Id, "supplier");
                    return RedirectToAction("Index", "Home");
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult suppliers()
        {

            List<ApplicationUser> l = new List<ApplicationUser>();
            foreach (var item in db.Users)
            {
                if (UserManager.IsInRole(item.Id, "supplier"))
                {
                    l.Add(item);
                }
            }

            return View(l);
        }

        public ActionResult delete_supplier(string id)
        {
            var x = db.Users.Find(id);
            db.Users.Remove(x);
            try
            {
                db.SaveChanges();
            }
            catch { }
            return RedirectToAction("suppliers");
        }
	}
}