namespace DHDCShop.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_MESSAGE : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Timestamp = c.String(),
                        FromUserId = c.String(maxLength: 128),
                        Type = c.Int(nullable: false),
                        Stick = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.FromUserId)
                .Index(t => t.FromUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "FromUserId", "dbo.Customers");
            DropIndex("dbo.Messages", new[] { "FromUserId" });
            DropTable("dbo.Messages");
        }
    }
}
