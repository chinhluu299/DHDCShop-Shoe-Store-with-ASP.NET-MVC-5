using DHDCShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHDCShop.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    public class ContactController : Controller
    {
        DHDCShopDbContext db = new DHDCShopDbContext();
        // GET: Admin/Contact
        public ActionResult Index(int? option)
        {
            try
            {
                if(option == 1)
                {
                    var data = db.Contacts.Where(x => x.Status == 1).OrderByDescending(x => x.Time).ToList();
                    ViewBag.HaveSeen = true;
                    return View(data);
                }
                else
                {
                    var data = db.Contacts.Where(x => x.Status == 0).OrderByDescending(x => x.Time).ToList();
                    ViewBag.HaveSeen = false;
                    return View(data);
                }
             
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }
        public ActionResult ContactChangeStatus(int contactID)
        {
            try
            {
                var contact = db.Contacts.Find(contactID);
                if(contact != null)
                {
                    contact.Status = 1;
                    db.SaveChanges();
                }
            } catch (Exception ex)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}