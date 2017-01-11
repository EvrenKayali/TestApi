namespace MoneyBox.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class build31 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CashierBranch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        BranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branch", t => t.BranchId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CashierBranch", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CashierBranch", "BranchId", "dbo.Branch");
            DropIndex("dbo.CashierBranch", new[] { "BranchId" });
            DropIndex("dbo.CashierBranch", new[] { "UserId" });
            DropTable("dbo.CashierBranch");
        }
    }
}
