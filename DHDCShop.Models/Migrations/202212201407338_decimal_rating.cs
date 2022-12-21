namespace DHDCShop.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class decimal_rating : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Rating", c => c.Int(nullable: false));
        }
    }
}
