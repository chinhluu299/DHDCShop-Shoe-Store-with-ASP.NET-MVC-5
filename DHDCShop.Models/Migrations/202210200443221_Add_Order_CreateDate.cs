﻿namespace DHDCShop.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Order_CreateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CreateDate");
        }
    }
}
