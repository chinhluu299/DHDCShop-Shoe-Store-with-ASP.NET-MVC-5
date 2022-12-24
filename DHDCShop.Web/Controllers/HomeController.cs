using DHDCShop.Models;
using DHDCShop.Models.Model;
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
            try
            {
                var top_giay = db.Products.OrderByDescending(s => s.Rating).Take(6).ToList();
                ViewBag.TopGiay = top_giay;
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("Error");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                if(contact.Name == null)
                {
                    ModelState.AddModelError("","Name is required");
                    return View(contact);
                }
                if (contact.Email == null)
                {
                    ModelState.AddModelError("", "Email is required");
                    return View(contact);
                }
                if (contact.Message == null)
                {
                    ModelState.AddModelError("", "Please type your messages");
                    return View(contact);
                }

                Contact newContact = new Contact()
                {
                    Name = contact.Name,
                    Email = contact.Email,
                    Message = contact.Message,
                    Time = DateTime.Now,
                    Status = 0,
                };
                db.Contacts.Add(newContact);
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult Error()
        {
            return View("Error");
        }
    }
}