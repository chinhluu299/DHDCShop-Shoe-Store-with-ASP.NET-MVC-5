using DHDCShop.Models;
using DHDCShop.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DHDCShop.Web.Controllers
{
    //[Authorize(Roles="user")]
    public class UserController : Controller
    {
        // GET: User
        DHDCShopDbContext db = new DHDCShopDbContext();
        // GET: User
        public ActionResult Index()
        {
            string username = Session["username"].ToString();
            Customer dangNhap = db.Customers.Find(username);
            ViewBag.Type = "profile";
            return View(dangNhap);
           
        }
        [AllowAnonymous]
        public ActionResult SignInUp()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(string Username, string Password)
        {
            if (ModelState.IsValid)
            {
                var data = db.Customers.Where(s => s.Username.Equals(Username) && 
                s.Password.Equals(Password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["username"] = data.FirstOrDefault().Username;
                    Session["password"] = data.FirstOrDefault().Password;
                    Session["type"] = "user";
                    FormsAuthentication.SetAuthCookie(data.FirstOrDefault().Username, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Username or password is incorrect";
                }
            }
            return View("SignInUp");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(Customer taiKhoan)
        {
            try
            {
                Customer tk = new Customer();
                tk.FullName = taiKhoan.FullName;
                tk.Username = taiKhoan.Username;
                tk.Password = taiKhoan.Password;
                tk.Email = taiKhoan.Email;
                tk.PhoneNumber = taiKhoan.PhoneNumber;
                tk.DateOfRegister = DateTime.Now.Date;
                tk.TotalSpent = 0;

                db.Customers.Add(tk);

                db.SaveChanges();
                ViewBag.Complete = "Sign up completed";
                return RedirectToAction("SignInUp");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorSignUp = ex.Message;
                return View("SignInUp");
            }

        }
        [HttpPost]
        public ActionResult UpdateProfile(HttpPostedFileBase file, Customer update)
        {
            try
            {
                string tendangnhap = update.Username;
                Customer taikhoan = db.Customers.Find(tendangnhap);

                taikhoan.FullName = update.FullName;
                taikhoan.Gender = update.Gender;
                taikhoan.Email = update.Email;
                taikhoan.Nation = update.Nation;
                taikhoan.PhoneNumber = update.PhoneNumber;
                taikhoan.DateOfBirth = update.DateOfBirth;
                taikhoan.Address = update.Address;

                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var ext = Path.GetExtension(file.FileName);
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = "avatar_" + tendangnhap + ext; //appending the name with id  
                                                                   // store the file inside ~/project folder(Img)  
                    var path = "~/Source/" + myfile;
                    var path2 = Path.Combine(Server.MapPath("~/Source"), myfile);

                    var path_del = Server.MapPath(taikhoan.AvatarPath);
                    FileInfo file2 = new FileInfo(path_del);
                    if (file2.Exists)//check file exsit or not  
                    {
                        file2.Delete();
                    }
                    taikhoan.AvatarPath = path;
                    file.SaveAs(path2);

                }

                db.SaveChanges();

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string reNewPassword)
        {
            string username = Session["username"].ToString();
            Customer dangNhap = db.Customers.Find(username);
            if (currentPassword.Length > 0 && newPassword.Length > 0 && reNewPassword.Length > 0)
            {
                if (dangNhap.Password.Equals(currentPassword))
                {
                    if (newPassword.Equals(reNewPassword))
                    {
                        dangNhap.Password = newPassword;
                        db.SaveChanges();
                        ViewBag.status = "Change password successful";
                        ViewBag.type = "password";
                        ViewBag.success = "true";
                        return View("Index", dangNhap);
                    }
                    else
                    {
                        ViewBag.status = "Confirm password not match";
                        ViewBag.type = "password";
                        return View("Index", dangNhap);
                    }
                }
                else
                {
                    ViewBag.status = "Current password is not true";
                    ViewBag.type = "password";
                    return View("Index", dangNhap);
                }
            }
            else
            {
                ViewBag.status = "Please enter full";
                ViewBag.type = "password";
                return View("Index", dangNhap);
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("SignInUp");
        }

        public ActionResult WishList()
        {
            if (Session["username"] != null)
            {
                if (Session["type"].Equals("user"))
                {
                    string username = Session["username"].ToString();
                    Customer user = db.Customers.Find(username);
                    List<WishList> wishList = user.WishLists.ToList();
                    List<Product> giay = new List<Product>();
                    foreach (var item in wishList)
                    {
                        giay.Add(db.Products.Find(item.ProductId));
                    }
                    return View(giay);
                }

            }
            return RedirectToAction("SignInUp");
        }
        [HttpPost]
        public JsonResult AddWishList(int magiay)
        {
            if (Session["username"] != null)
            {
                if (Session["type"].Equals("user"))
                {
                    string username = Session["username"].ToString();
                    WishList kiemtra = db.WishLists.Where(s => s.CustomerUsername == username && s.ProductId == magiay).FirstOrDefault();
                    if (kiemtra == null)
                    {
                        WishList taoMoi = new WishList();
                        taoMoi.CustomerUsername = username;
                        taoMoi.ProductId = magiay;

                        db.WishLists.Add(taoMoi);
                        db.SaveChanges();
                    }
                    return Json(new { result = "true" });

                }

            }
            return Json(new { result = "false" });
        }

        public ActionResult RemoveWLItem(int magiay)
        {
            if (Session["username"] != null)
            {
                if (Session["type"].Equals("user"))
                {
                    string username = Session["username"].ToString();
                    WishList kiemtra = db.WishLists.Where(s => s.CustomerUsername == username && s.ProductId == magiay).FirstOrDefault();
                    if (kiemtra != null)
                    {
                        db.WishLists.Remove(kiemtra);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Wishlist");

                }

            }
            return null;
        }
    }
}