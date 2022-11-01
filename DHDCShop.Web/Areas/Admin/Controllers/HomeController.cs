using DHDCShop.Models;
using DHDCShop.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DHDCShop.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        private DHDCShopDbContext db = new DHDCShopDbContext();
        public ActionResult Index()
        {
            if(Session["username"] != null)
            {
                if (Session["type"].Equals("admin"))
                {
                    return RedirectToAction("Index","Dashboard");
                } 
            }
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var username = login.Username;
                var password = login.Password; 
                var data = db.Admins.Where(s => s.Username.Equals(username) && s.Password.Equals(password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["username"] = data.FirstOrDefault().Username;
                    Session["password"] = data.FirstOrDefault().Password;
                    Session["type"] = "admin";
                    FormsAuthentication.SetAuthCookie(data.FirstOrDefault().Username, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
