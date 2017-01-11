namespace MoneyBox.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class build5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MoneyBoxTransactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        BranchId = c.Int(),
                        PurchaseAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransferAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FromAccountId = c.Int(),
                        ToAccountId = c.Int(),
                        CampaignId = c.Int(),
                        IsPurchase = c.Boolean(nullable: false),
                        IsReverseTransaction = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Campaign", t => t.CampaignId)
                .ForeignKey("dbo.MoneyBoxAccount", t => t.FromAccountId)
                .ForeignKey("dbo.MoneyBoxAccount", t => t.ToAccountId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FromAccountId)
                .Index(t => t.ToAccountId)
                .Index(t => t.CampaignId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MoneyBoxTransactions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MoneyBoxTransactions", "ToAccountId", "dbo.MoneyBoxAccount");
            DropForeignKey("dbo.MoneyBoxTransactions", "FromAccountId", "dbo.MoneyBoxAccount");
            DropForeignKey("dbo.MoneyBoxTransactions", "CampaignId", "dbo.Campaign");
            DropIndex("dbo.MoneyBoxTransactions", new[] { "CampaignId" });
            DropIndex("dbo.MoneyBoxTransactions", new[] { "ToAccountId" });
            DropIndex("dbo.MoneyBoxTransactions", new[] { "FromAccountId" });
            DropIndex("dbo.MoneyBoxTransactions", new[] { "UserId" });
            DropTable("dbo.MoneyBoxTransactions");
        }
    }
}
