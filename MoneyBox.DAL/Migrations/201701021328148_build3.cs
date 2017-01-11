namespace MoneyBox.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class build3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Campaign", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.MoneyBoxAccount", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MoneyBoxAccount", "Amount");
            DropColumn("dbo.Campaign", "IsActive");
        }
    }
}
