namespace DHDCShop.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Order_AddressReceive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "AddressReceive", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "AddressReceive");
        }
    }
}
