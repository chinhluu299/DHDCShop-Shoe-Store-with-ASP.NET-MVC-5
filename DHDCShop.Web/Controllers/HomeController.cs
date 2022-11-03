using DHDCShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHDCShop.Web.Controllers
{
    public class HomeController : Controller
    {
        DHDCShopDbContext db = new DHDCShopDbContext();
        public ActionResult Index()
        {
            var top_giay = db.Products.OrderByDescending(s => s.Rating).Take(6).ToList();
            ViewBag.TopGiay = top_giay;
            return View();
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