using DHDCShop.Models;
using DHDCShop.Models.Model;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(DHDCShop.Web.Startup))]
namespace DHDCShop.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            CreateAdminAccount();
            CreateStatusType();
            app.MapSignalR();
        }

        private void CreateStatusType()
        {
            DHDCShopDbContext db = new DHDCShopDbContext();
            var check1= db.Statuses.Where(x => x.Name=="pending").FirstOrDefault();
            var check2 = db.Statuses.Where(x => x.Name == "shipping").FirstOrDefault();
            var check3 = db.Statuses.Where(x => x.Name == "completed").FirstOrDefault();
            var check4 = db.Statuses.Where(x => x.Name == "cancel").FirstOrDefault();


            if (check1== null)
            {
                Status status = new Status();
                status.ID = 1;
                status.Name = "pending";
                status.Description = "The order is pending and have to be submited by admin";

                db.Statuses.Add(status);
                db.SaveChanges();
            }
            if (check2 == null)
            {
                Status status = new Status();
                status.ID = 2;
                status.Name = "shipping";
                status.Description = "The order is shipping and a shipper is taking to the customer";

                db.Statuses.Add(status);
                db.SaveChanges();
            }
            if (check3 == null)
            {
                Status status = new Status();
                status.ID = 3;
                status.Name = "completed";
                status.Description = "The order is shipped completely";

                db.Statuses.Add(status);
                db.SaveChanges();
            }
            if (check4 == null)
            {
                Status status = new Status();
                status.ID = 4;
                status.Name = "cancel";
                status.Description = "The order is canceled by the customer";

                db.Statuses.Add(status);
                db.SaveChanges();
            }
        }

        private void CreateAdminAccount()
        {
            DHDCShopDbContext db = new DHDCShopDbContext();
            var check = db.Admins.Where(x => x.Username == "admin").FirstOrDefault();
            if(check == null)
            {
                Admin admin = new Admin();
                admin.Username = "admin";
                admin.Password = "Aa@123";
                admin.Email = "20521133@gm.uit.edu.vn";
                admin.PhoneNumber = "0911285999";
                db.Admins.Add(admin);
                db.SaveChanges();
            }
            
        }
    }
}