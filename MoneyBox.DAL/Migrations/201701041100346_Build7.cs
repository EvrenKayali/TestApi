namespace MoneyBox.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Build7 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserIdentifiers", newName: "UserIdentifier");
            AlterColumn("dbo.UserIdentifier", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserIdentifier", "UserId", c => c.String());
            RenameTable(name: "dbo.UserIdentifier", newName: "UserIdentifiers");
        }
    }
}
