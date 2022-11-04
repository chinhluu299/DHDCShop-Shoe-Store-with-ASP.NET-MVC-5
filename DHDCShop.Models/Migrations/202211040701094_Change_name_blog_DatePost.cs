namespace DHDCShop.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_name_blog_DatePost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "DatePost", c => c.DateTime(nullable: false));
            DropColumn("dbo.Blogs", "NgayDang");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blogs", "NgayDang", c => c.DateTime(nullable: false));
            DropColumn("dbo.Blogs", "DatePost");
        }
    }
}
