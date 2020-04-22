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
using System.Net.Mail;


namespace WebApplication9.Controllers
{
    public class productController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //public static List<product> products_before_compagins ;

        // GET: /product/
        public UserManager<ApplicationUser> UserManager { get; private set; }
        
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
                List<product> l = new List<product>();
            foreach (var item in db.products)
            {
                if(item.supplierId.Equals(id))
                {
                    l.Add(item);
                }
            }
            return View(l);
        }

        // GET: /product/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.productid = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /product/Create
         [Authorize(Roles = "supplier")]
        public ActionResult Create()
        {
           
            return View();
        }
     

        // POST: /product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

       
        public ActionResult Create( product product,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            { string path=Path.Combine(Server.MapPath("~/pics"),upload.FileName);
                upload.SaveAs(path);
                product.image = upload.FileName;
                product.supplierId = User.Identity.GetUserId();
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           
            return View(product);
        }
        public List<ApplicationUser> deliveryboys;
        public void get_deliveryboys()
        {
             UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
  
            deliveryboys = new List<ApplicationUser>();
            foreach (var item in db.Users)
            {
                if (UserManager.IsInRole(item.Id, "deliveryboy")) { deliveryboys.Add(item); }
            }

        }
       
        [HttpPost]
        public ActionResult sendmail(ApplicationUser user,orders o)
        {
            var mail = new MailMessage();
            var login = new NetworkCredential("mohamedalaa447447@gmail.com", "mohamed447");
            mail.From = new MailAddress("mohamedalaa447447@gmail.com");
            mail.To.Add(new MailAddress(user.Email));
            mail.Subject = "Order";
            var cust =db.Users.Find(o.custId);

            mail.Body = ("you have order from :" + cust.UserName + "<br>" + "Delivery address is :" + cust.Delivery_address + "<br/>" + "Customer number is :" + cust.phone);
            var smtpclient = new SmtpClient("smtp.gmail.com",587);
            smtpclient.EnableSsl = true;
            smtpclient.Credentials = login;
            smtpclient.UseDefaultCredentials = true;
            smtpclient.Send(mail);
            return RedirectToAction("Index");
        }
        public ActionResult make_order()
        {

            return View();
        }
        public ActionResult seasonal_compaigns()
        {
           

            return View();
        }
        /* [HttpPost]
         public ActionResult seasonal_compaigns(double rate)
         {
              products_before_compagins = new List<product>();
             products_before_compagins = db.products.ToList();
             foreach (var item in db.products)
             {
                 item.price -=(int) ((rate / 100) * item.price);
                  db.Entry(item).State = EntityState.Modified;
               
             }
             db.SaveChanges();
             return View();
         }*/
        [HttpPost]
       
       
        
        
        
     public ActionResult make_order(orders o)
        {
            get_deliveryboys();
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
             foreach (var item in deliveryboys)
            {
                if (item.line == currentUser.line) { o.deleveryboyId = item.Id; break; }
            }
             
            
           
             o.custId = User.Identity.GetUserId();
             o.productId = (int)Session["productid"];

             var d = db.Users.Find(o.deleveryboyId);
             db.orders.Add(o);
             product product = db.products.Find(o.productId);
             product.stock--;
             db.Entry(product).State = EntityState.Modified;
             if (product.stock <= 0) db.products.Remove(product);
;             db.SaveChanges();
             //sendmail(d, o);
             return RedirectToAction("Index","Home");
        }
      
     
        // GET: /product/Edit/5
         [Authorize(Roles = "supplier")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ewBag.supplierId = new SelectList(db.IdentityUsers, "Id", "UserName", product.supplierId);
            return View(product);
        }

        // POST: /product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,name,price,description,image,supplierId")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           //iewBag.supplierId = new SelectList(db.IdentityUsers, "Id", "UserName", product.supplierId);
            return View(product);
        }

        // GET: /product/Delete/5
         [Authorize(Roles = "supplier")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
