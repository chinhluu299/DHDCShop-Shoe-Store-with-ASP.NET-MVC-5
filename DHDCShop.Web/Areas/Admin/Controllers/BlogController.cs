using DHDCShop.Common;
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
    public class BlogController : Controller
    {
        private DHDCShopDbContext db = new DHDCShopDbContext();

        // GET: Admin/BLOGs
        public ActionResult Index()
        {
          
                return View(db.Blogs.ToList());
         

        }

        // GET: Admin/BLOGs/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BLOG bLOG = db.BLOGs.Find(id);
        //    if (bLOG == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bLOG);
        //}

        // GET: Admin/BLOGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BLOGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Title,Content")] Blog blog, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                blog.DatePost = DateTime.Now;
                blog.Content = StringEncodeHelper.Base64Encode(blog.Content);
                db.Blogs.Add(blog);
                db.SaveChanges();

                var getGiay = db.Blogs.Select(item => item).OrderByDescending(item => item.ID).FirstOrDefault();
                var id = getGiay.ID;

                if (image != null)
                {
                    var ext = Path.GetExtension(image.FileName);
                    string myfile = "blog_" + id + ext; //appending the name with id  
                                                        // store the file inside ~/project folder(Img)  
                    var path = "~/Source/Blog/" + myfile;
                    var path2 = Path.Combine(Server.MapPath("~/Source/Blog"), myfile);
                    getGiay.ImagePath = path;
                    image.SaveAs(path2);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: Admin/BLOGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Admin/BLOGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, [Bind(Include = "Title,Content")] Blog bLOG, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var getUpd = db.Blogs.Find(id);
                getUpd.Content = StringEncodeHelper.Base64Encode(bLOG.Content);
                getUpd.Title = bLOG.Title;

                if (image != null)
                {
                    var ext = Path.GetExtension(image.FileName);
                    string myfile = "blog_" + id + ext;

                    var path = "~/Source/Blog/" + myfile;
                    var path2 = Path.Combine(Server.MapPath("~/Source/Blog"), myfile);
                    //delete
                    var path_del = Server.MapPath(getUpd.ImagePath);
                    FileInfo file2 = new FileInfo(path_del);
                    if (file2.Exists)//check file exsit or not  
                    {
                        file2.Delete();
                    }
                    //add new
                    getUpd.ImagePath = path;
                    image.SaveAs(path2);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bLOG);
        }

        // GET: Admin/BLOGs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BLOG bLOG = db.BLOGs.Find(id);
        //    if (bLOG == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bLOG);
        //}

        // POST: Admin/BLOGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            var path_del = Server.MapPath(blog.ImagePath);
            FileInfo image = new FileInfo(path_del);

            if (image.Exists)//check file exsit or not  
            {
                image.Delete();
            }

            db.Blogs.Remove(blog);
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