using DHDCShop.Models;
using DHDCShop.Models.Model;
using DHDCShop.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHDCShop.Web.Controllers
{
    [Authorize(Roles ="User")]
    public class PurchasingController : Controller
    {
        // GET: Purchasing
        DHDCShopDbContext db = new DHDCShopDbContext();

        private const string SESSIONCART = "cart";
        // GET: Purchasing
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var customer = db.Customers.Find(username);
            return View(customer);
        }



        public ActionResult Payment(FormCollection form)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.form = form;
                PaymentViewModel statePay = new PaymentViewModel();
                statePay.Phone = form["phone"];
                statePay.Email = form["email"];
                statePay.Fullname = form["fullname"];
                statePay.Country = form["country"];
                statePay.City = form["city"];
                statePay.Address = form["address"];
                statePay.Apartment = form["apartment"];
                statePay.ZipCode = form["zip"];
                Session["payment"] = statePay;

                return View();
            }
            else
            {
                return RedirectToAction("SignInUp", "User");
            }

        }
        //Cart
        [AllowAnonymous]
        public PartialViewResult AddNewItemToCart(int productId, int size, int quantity)
        {
            var cart = Session[SESSIONCART];
            if (cart != null)
            {
                var list = (List<CartItemViewModel>)cart;
                if (list.Exists(x => (x.Product.ProductId == productId && x.Size == size)))
                {
                    var update = list.Find(x => x.Product.ProductId == productId && x.Size == size);
                    update.Quantity += quantity;
                }
                else
                {
                    CartItemViewModel item = new CartItemViewModel();
                    item.Product = db.Products.Find(productId);
                    item.Quantity = quantity;
                    item.Size = size;
                    list.Add(item);
                }
                Session[SESSIONCART] = list;
            }
            else
            {
                CartItemViewModel item = new CartItemViewModel();
                item.Product = db.Products.Find(productId);
                item.Quantity = quantity;
                item.Size = size;

                List<CartItemViewModel> listItem = new List<CartItemViewModel>();
                listItem.Add(item);
                Session[SESSIONCART] = listItem;
            }

            //return RedirectToAction("Index", "Collection");
            return PartialView("_Cart");

        }

        [HttpPost]
        [AllowAnonymous]

        public void AddItemToCartItem(int productId, int size, int quantity)
        {
            var cart = Session[SESSIONCART];
            if (cart != null)
            {
                var list = (List<CartItemViewModel>)cart;
                if (list.Exists(x => x.Product.ProductId == productId && x.Size == size))
                {
                    var update = list.Find(x => x.Product.ProductId == productId && x.Size == size);
                    if (update.Quantity + quantity >= 0)
                        update.Quantity += quantity;
                    else
                    {
                        update.Quantity = 0;
                    }
                }
                Session[SESSIONCART] = list;
            }

        }


        [HttpPost]
        [AllowAnonymous]

        public JsonResult RemoveItemFromCart(int productId, int size)
        {
            var cart = Session[SESSIONCART];
            var list = new List<CartItemViewModel>();

            int index_item = -1;
            decimal giatong = 0;


            if (cart != null)
            {
                list = (List<CartItemViewModel>)cart;
                if (list.Exists(x => x.Product.ProductId == productId && x.Size == size))
                {
                    var update = list.Find(x => x.Product.ProductId == productId && x.Size == size);
                    index_item = list.IndexOf(update);

                    if (update != null)
                    {
                        list.Remove(update);
                    }
                }

                Session[SESSIONCART] = list;
            }
            foreach (var i in list)
            {
                giatong += i.Product.Price * i.Quantity;
            }

            return Json(new
            {
                danhsach = index_item,
                giatong = giatong
            });
        }

        [HttpPost]
        public ActionResult PaymentLaterAction()
        {
            if (User.Identity.IsAuthenticated)
            {
                Order newOrder = new Order();
                PaymentViewModel payment = (PaymentViewModel)Session["payment"];
                List<CartItemViewModel> listItemPay = (List<CartItemViewModel>)Session["cart"];

                if (payment != null)
                {
                    newOrder.NumberPhoneRev = payment.Phone;
                    newOrder.EmailRev = payment.Email;
                    newOrder.NameOfReceiver = payment.Fullname;
                    newOrder.AddressReceive = payment.Apartment + " - " + payment.Address + " - " + payment.City + " - " + payment.Country;
                    newOrder.StatusId = 1;
                    newOrder.IsPaid = false;
                    newOrder.ZipCode = payment.ZipCode;
                    newOrder.CustomerId = User.Identity.Name;
                    newOrder.TotalMoney = 0;
                    newOrder.CreateDate = DateTime.Now;
                    db.Orders.Add(newOrder);
                    db.SaveChanges();

                    foreach (var item in listItemPay)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderId = newOrder.OrderId;
                        orderDetail.ProductId = item.Product.ProductId;
                        orderDetail.Size = item.Size;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.Price = item.Product.Price * item.Quantity;
                        newOrder.TotalMoney += orderDetail.Price;

                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();


                        ProductSize bangSize = db.ProductSizes.Where(s => s.ProductId == orderDetail.ProductId && s.Size == orderDetail.Size).FirstOrDefault();

                        bangSize.Quantity -= orderDetail.Quantity;
                        db.SaveChanges();
                    }

                    Session["cart"] = null;
                    string status = "completed";
                    return RedirectToAction("PaymentResult", new { status = status });

                }
                else
                {
                    string status = "failed";
                    return RedirectToAction("PaymentResult", new { status = status });
                }
            }
            else
            {
                return RedirectToAction("SignInUp", "User");
            }


        }
        public ActionResult PaymentResult(string status)
        {

            ViewBag.status = status;
            return View();
        }
    }
}