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
    [Authorize(Roles ="user")]
    public class OrderController : Controller
    {
        // GET: Order
        DHDCShopDbContext db = new DHDCShopDbContext();
        // GET: Order
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                List<Order> orderList = db.Orders.Where(x => x.CustomerId == username).ToList();
                List<OrderItemViewModel> orderListViewModel = new List<OrderItemViewModel>();


                foreach (var order in orderList)
                {
                    List<CartItemViewModel> cartItems = new List<CartItemViewModel>();
                    foreach (var detail in order.OrderDetails)
                    {
                        CartItemViewModel icart = new CartItemViewModel();
                        icart.Product = db.Products.Find(detail.ProductId);
                        icart.Quantity = detail.Quantity;
                        icart.Size = detail.Size;
                        cartItems.Add(icart);
                    }
                    OrderItemViewModel newOrder = new OrderItemViewModel();
                    newOrder.ListCartItem =cartItems;
                    newOrder.Order = order;
                    orderListViewModel.Add(newOrder);

                }
                return View(orderListViewModel.OrderByDescending(s => s.Order.CreateDate));
            }
            else
                return RedirectToAction("SignInUp", "User");
        }

        [HttpPost]
        public void CancelOrder(int orderId)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var username = User.Identity.Name;
                    Order order = db.Orders.Where(x => x.OrderId == orderId && x.CustomerId == username).FirstOrDefault();
                    order.StatusId = 4;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult ReOrder(int orderId)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    foreach (var detail in db.OrderDetails.Where(s => s.OrderId == orderId).ToList())
                    {
                        var productId = detail.ProductId;
                        var size = detail.Size;
                        var quantity = detail.Quantity;

                        var cart = Session["cart"];
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
                            Session["cart"] = list;
                        }
                        else
                        {
                            CartItemViewModel item = new CartItemViewModel();
                            item.Product = db.Products.Find(productId);
                            item.Quantity = quantity;
                            item.Size = size;

                            List<CartItemViewModel> listItem = new List<CartItemViewModel>();
                            listItem.Add(item);
                            Session["cart"] = listItem;
                        }

                    }
                    return RedirectToAction("Cart");
                }

                catch (Exception e)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("SignInUp", "User");
            }
        }

        public ActionResult ReviewOrder(int orderId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                Order hoadon = db.Orders.Where(x => x.OrderId == orderId && x.CustomerId == username).FirstOrDefault();
                if (hoadon != null)
                    return View(hoadon);
                else return View("Error");
            }
            else
            {
                return RedirectToAction("SignInUp", "User");
            }
        }

        [HttpPost]
        public void AddReview(int productId, int numberOfStar, string comment, int orderId)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var username = User.Identity.Name;
                    Comment cmt = new Comment();

                    cmt.ProductId = productId;
                    cmt.CustomerUsername = username;
                    cmt.CreatedDate = DateTime.Now;
                    cmt.Comments = comment;

                    db.Comments.Add(cmt);

                    Rating rate = new Rating();
                    rate.ProductId = productId;
                    rate.CustomerUsername = username;
                    rate.NumberOfStar = numberOfStar;
                    rate.OrderId = orderId;

                    db.Ratings.Add(rate);

                    db.SaveChanges();

                }
                catch (Exception e)
                {
                }
            }
        }
    }
}