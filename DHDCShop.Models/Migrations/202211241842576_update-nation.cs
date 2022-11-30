namespace DHDCShop.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Statistics", "AverageMoney", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Statistics", "NumberOfOrder");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Statistics", "NumberOfOrder", c => c.Int(nullable: false));
            DropColumn("dbo.Statistics", "AverageMoney");
        }
    }
}
