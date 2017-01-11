namespace MoneyBox.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class build10 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MoneyBoxTransactions", "OldAmount");
            DropColumn("dbo.MoneyBoxTransactions", "NewAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MoneyBoxTransactions", "NewAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MoneyBoxTransactions", "OldAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
