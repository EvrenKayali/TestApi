namespace MoneyBox.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class build2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaign",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 75),
                        CompanyId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DiscountPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.MoneyBoxAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CompanyId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MoneyBoxAccount", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MoneyBoxAccount", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Campaign", "CompanyId", "dbo.Company");
            DropIndex("dbo.MoneyBoxAccount", new[] { "UserId" });
            DropIndex("dbo.MoneyBoxAccount", new[] { "CompanyId" });
            DropIndex("dbo.Campaign", new[] { "CompanyId" });
            DropTable("dbo.MoneyBoxAccount");
            DropTable("dbo.Campaign");
        }
    }
}
