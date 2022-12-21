using DHDCShop.Models;
using DHDCShop.Models.Model;
using PagedList;
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

        public PartialViewResult _IndexFilter(int? page, string brand, string stars, string status)
        {
            ViewData["filter-brand"] = brand;
            ViewData["filter-stars"] = stars;
            ViewData["filter-status"] = status;
            //step 1
            brand = (brand != null) ? brand : "";
            var links = db.Products.OrderBy(s => s.ProductId).ToList();
            if (!brand.Equals(""))
            {
                links = db.Products.Where(s => brand.ToLower().Contains(s.Brand.ToLower().Trim())).OrderBy(s => s.ProductId).ToList();
            }

            //step 2
            stars = (stars != null) ? stars : "";
            if (!stars.Equals(""))
            {
                links = links.Where(s => stars.Contains(((int)s.Rating).ToString())).OrderBy(s => s.ProductId).ToList();
            }

            //step 3
            status = (status != null) ? status : "";
            if (!status.Equals(""))
            {
                var links_temp_1 = links;
                var links_temp_2 = links;
                var links_temp_3 = links;
                HashSet<Product> links_4 = new HashSet<Product>();

                if (status.Contains("1"))
                {

                    var Sales = db.SaleOffs.Where(s => DateTime.Compare(s.DateStart, DateTime.UtcNow) < 0 &&
                                                        DateTime.Compare(DateTime.UtcNow, s.DateEnd) < 0)
                                            .Select(s => s.ProductId);
                    links_temp_1 = links_temp_1.Where(s => Sales.Contains(s.ProductId)).OrderBy(s => s.ProductId).ToList();
                    foreach (var i in links_temp_1)
                    {
                        links_4.Add(i);
                    }
                }
                if (status.Contains("2"))
                {
                    links_temp_2 = links_temp_2.Where(s => s.Quantity > 0).OrderBy(s => s.ProductId).ToList();
                    foreach (var i in links_temp_2)
                    {
                        links_4.Add(i);
                    }
                }
                if (status.Contains("3"))
                {
                    links_temp_3 = links_temp_3.Where(s => s.Quantity == 0).OrderBy(s => s.ProductId).ToList();
                    foreach (var i in links_temp_3)
                    {
                        links_4.Add(i);
                    }
                }
                links = links_4.OrderBy(s => s.ProductId).ToList();

            }


            int pageNumber = (page ?? 1);
            var listItem = links.ToList();

            ViewBag.listItem = listItem;
            var pagedlist = links.ToPagedList(pageNumber, 6);

            return PartialView(pagedlist);
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