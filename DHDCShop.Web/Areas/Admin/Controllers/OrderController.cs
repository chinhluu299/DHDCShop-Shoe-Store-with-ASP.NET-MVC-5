using DHDCShop.Models;
using DHDCShop.Models.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DHDCShop.Web.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        private DHDCShopDbContext db = new DHDCShopDbContext();

        // GET: Admin/Order
        public ActionResult Index()
        {
             var orders = db.Orders.ToList();
             return View(orders);
        }

        public PartialViewResult _OrderTable(int? page, string filter)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            filter = (filter == null || filter == "") ? "" : filter;
            var links = db.Orders.Where(s => s.Status.Name.ToLower().Contains(filter)).ToList();
            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            ViewBag.Filter = filter;

            return PartialView("_OrderTable", links.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
          
            Order order = db.Orders.Find(id);
            return View(order);
       
        }

        // GET: Admin/Order/Create
        //public ActionResult Create()
        //{
        //    ViewBag.khachhang = new SelectList(db.TAIKHOANs, "tendangnhap", "matkhau");
        //    return View();
        //}

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "sohd,khachhang,trigia,ngaythanhtoan")] HOADON hOADON)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.HOADONs.Add(hOADON);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.khachhang = new SelectList(db.TAIKHOANs, "tendangnhap", "matkhau", hOADON.khachhang);
        //    return View(hOADON);
        //}

        // GET: Admin/Order/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.khachhang = new SelectList(db.Customers, "tendangnhap", "matkhau", order.Customer);
        //    return View(order);
        //}

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "sohd,khachhang,trigia,ngaythanhtoan")] HOADON hOADON)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(hOADON).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.khachhang = new SelectList(db.TAIKHOANs, "tendangnhap", "matkhau", hOADON.khachhang);
        //    return View(hOADON);
        //}

        // GET: Admin/Order/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            var listOrderDetail = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            db.OrderDetails.RemoveRange(listOrderDetail);

            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

        public ActionResult Approve(int id)
        {
                Order order = db.Orders.Find(id);
                order.StatusId = 2; // shipping
                db.SaveChanges();
                return View(order);
       
        }
        public ActionResult Confirm(int id)
        {
           
                Order order = db.Orders.Find(id);
                order.StatusId = 3; // Completed
                db.SaveChanges();
                return View(order);
          
        }
    }
}