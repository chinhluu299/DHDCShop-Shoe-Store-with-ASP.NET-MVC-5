namespace DHDCShop.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Comment_CreateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "CreatedDate");
        }
    }
}
