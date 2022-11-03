using DHDCShop.Models;
using DHDCShop.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DHDCShop.Web.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        private DHDCShopDbContext db = new DHDCShopDbContext();

        // GET: Admin/Customer
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                return View(db.Customers.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        // GET: Admin/Customer/Details/5
        public ActionResult Details(string id)
        {
            if (Session["username"] != null)
            {
                if (db.Customers.Find(id) != null)
                    return View(db.Customers.Find(id));
                else return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Admin/Customer/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/Customer/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "C_id,tendangnhap,matkhau,hoten,email,gioitinh,sdt,diachi,ngaysinh,ngaydk,avatar")] TAIKHOAN tAIKHOAN)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TAIKHOANs.Add(tAIKHOAN);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(tAIKHOAN);
        //}

        // GET: Admin/Customer/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
        //    if (tAIKHOAN == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tAIKHOAN);
        //}

        //// POST: Admin/Customer/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "C_id,tendangnhap,matkhau,hoten,email,gioitinh,sdt,diachi,ngaysinh,ngaydk,avatar")] TAIKHOAN tAIKHOAN)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tAIKHOAN).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(tAIKHOAN);
        //}

        // GET: Admin/Customer/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        //// POST: Admin/Customer/Delete/5
        //[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
            
        //    Customer customer = db.Customers.Find(id);
        //    db.Customers.Remove(customer);
        //    db.SaveChanges();

        //    var path = Server.MapPath(customer.AvatarPath);
        //    FileInfo file = new FileInfo(path);
        //    if (file.Exists)//check file exsit or not  
        //    {
        //        file.Delete();
        //    }

        //    return RedirectToAction("Index");
        //}

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