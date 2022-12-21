using DHDCShop.Models;
using DHDCShop.Models.Model;
using DHDCShop.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHDCShop.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        private DHDCShopDbContext db = new DHDCShopDbContext();
        public ActionResult Index()
        {
            try
            {
                DashboardViewModel dashboardVM = new DashboardViewModel();

                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                var kq = db.Statistics.Where(s => s.Month == month).Where(s => s.Year == year).FirstOrDefault();

                //tong doanh thu
                var listOrder = db.Orders.Where(s => s.CreateDate.Year == year && s.CreateDate.Month == month).ToList();
                decimal totalMoney = 0;
                decimal totalMoneyHasPaid = 0;
                foreach (var item in listOrder)
                {
                    totalMoney += item.TotalMoney;
                    if (item.IsPaid)
                    {
                        totalMoneyHasPaid += item.TotalMoney;
                    }
                }
                int sumOrder = listOrder.Count;
                decimal average = sumOrder > 0 ? totalMoney / sumOrder : 0;
                int newRegister = db.Customers.Where(s => s.DateOfRegister.Year == year && s.DateOfRegister.Month == month == true).ToList().Count;

                if (kq == null)
                {
                    Statistic statistic = new Statistic();
                    statistic.AverageMoney = average;
                    statistic.Year = year;
                    statistic.Month = month;
                    statistic.TotalRevenue = totalMoneyHasPaid;
                    statistic.NumberOfSales = sumOrder;
                    statistic.NumberOfNewRegister = newRegister;
                    db.Statistics.Add(statistic);

                    dashboardVM.StatisticIsNow = statistic;
                }
                else
                {
                    Statistic statistic = kq;
                    statistic.AverageMoney = average;
                    statistic.Year = year;
                    statistic.Month = month;
                    statistic.TotalRevenue = totalMoneyHasPaid;
                    statistic.NumberOfSales = sumOrder;
                    statistic.NumberOfNewRegister = newRegister;

                    dashboardVM.StatisticIsNow = statistic;
                }
                db.SaveChanges();

                //List<decimal> list_data = new List<decimal>();
                //List<decimal> list_data_quocgia = new List<decimal>();
                //List<string> list_data_tenquocgia = new List<string>();


                var statistics = db.Statistics.Where(s => s.Year == year).OrderBy(s => s.Month).ToList();
                dashboardVM.ListNationStatistic = db.Orders.Where(s => s.CreateDate.Year == year && s.CreateDate.Month == month && s.StatusId == 3)
                                                    .Join(db.Customers, p => p.CustomerId, q => q.Username, (p, q) => new { quoctich = q.Nation, doanhthu = p.TotalMoney })
                                                    .GroupBy(s => s.quoctich)
                                                    .Select(s => new NationStatisticViewModel { Nation =  s.FirstOrDefault().quoctich, Revenue = s.Sum(k => k.doanhthu) })
                                                    .OrderByDescending(x => x.Revenue)
                                                    .ToList();

                dashboardVM.ListTopSales = db.Orders.Where(s => s.CreateDate.Year == year && s.CreateDate.Month == month && s.StatusId == 3)
                                            .Join(db.OrderDetails, p => p.OrderId, q => q.OrderId, (q, p) => new { productId = p.ProductId, quantity = p.Quantity })
                                            .Join(db.Products, p => p.productId, q => q.ProductId, (q, p) => new { productId = p.ProductId, name = p.Name, quantity = q.quantity, imagePath = p.ImagePath })
                                            .GroupBy(s => s.productId)
                                            .Select(s => new TopSaleViewModel { ProductId = s.FirstOrDefault().productId, ImageUrl = s.FirstOrDefault().imagePath, ProductName = s.FirstOrDefault().name, Quantity = s.Sum(k => k.quantity) })
                                            .OrderByDescending(s => s.Quantity)
                                            .Take(10)
                                            .ToList();

                var listNameNation = dashboardVM.ListNationStatistic.Select(x => x.Nation).ToList();
                var listDataNation = dashboardVM.ListNationStatistic.Select(x => x.Revenue).ToList();


                for (int i = 1; i <= 12; i++)
                {
                    int k = 0;
                    foreach (var item in statistics)
                    {
                        if (item.Month == i)
                        {
                            k = 1;
                            dashboardVM.AverageEachMonth.Add(item.TotalRevenue);
                            break;
                        }
                    }
                    if (k == 0)
                    {
                        dashboardVM.AverageEachMonth.Add(0);
                    }
                }


                ViewBag.listNameNation = listNameNation;
                ViewBag.listDataNation = listDataNation;


                return View(dashboardVM);
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View("Error");
            }
         
        }
    }
}