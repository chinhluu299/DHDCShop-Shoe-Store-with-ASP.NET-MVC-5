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