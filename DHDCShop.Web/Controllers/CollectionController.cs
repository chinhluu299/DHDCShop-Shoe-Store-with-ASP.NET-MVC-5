using DHDCShop.Models;
using DHDCShop.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHDCShop.Web.Controllers
{
    public class CollectionController : Controller
    {
        DHDCShopDbContext db = new DHDCShopDbContext();
        // GET: Collection
        public ActionResult Index()
        {
            var listProduct = db.Products.ToList();
            var listBrands = db.Products.Select(x => x.Brand).Distinct().ToList();
            ViewBag.brands = listBrands;
            return View(listProduct);
        }

        public ActionResult Item(int id)
        {
            Product item = db.Products.Find(id);
            List<Rating> danhgia = db.Ratings.Where(s => s.ProductId == id).ToList();

            ViewBag.listDanhGia = danhgia;
            ViewBag.item = item;
            return View(item);
        }
    }
}