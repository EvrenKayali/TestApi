namespace MoneyBox.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class build9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MoneyBoxTransactions", "OldAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MoneyBoxTransactions", "NewAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MoneyBoxTransactions", "NewAmount");
            DropColumn("dbo.MoneyBoxTransactions", "OldAmount");
        }
    }
}
