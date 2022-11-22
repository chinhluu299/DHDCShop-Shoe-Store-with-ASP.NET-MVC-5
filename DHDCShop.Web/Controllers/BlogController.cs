using DHDCShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHDCShop.Web.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        DHDCShopDbContext db = new DHDCShopDbContext();
        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Blogs.ToList());
        }

        public ActionResult Detail(int id)
        {
            var blog = db.Blogs.Find(id);
            if (blog != null)
            {
                return View(blog);
            }
            return View("Index");
        }
    }
}