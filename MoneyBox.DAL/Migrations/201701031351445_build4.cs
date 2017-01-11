namespace MoneyBox.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class build4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CashierBranch", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CashierBranch", "Creator", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CashierBranch", "Creator");
            DropColumn("dbo.CashierBranch", "CreationDate");
        }
    }
}
