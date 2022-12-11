using DHDCShop.Models.Model;
using System;
using System.Data.Entity;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace DHDCShop.Models
{
    public class DHDCShopDbContext : DbContext
    {
        // Your context has been configured to use a 'DHDCShopDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DHDCShop.Models.DHDCShopDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DHDCShopDbContext' 
        // connection string in the application configuration file.
        public DHDCShopDbContext()
            : base("name=DHDCShopDbContext")
        {
      
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<SaleOff> SaleOffs { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Message> Messages { get; set; }


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}