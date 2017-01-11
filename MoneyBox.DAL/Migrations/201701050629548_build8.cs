namespace MoneyBox.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class build8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserIdentifier", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserIdentifier", "Expire", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserIdentifier", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserIdentifier", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserIdentifier", "Expire");
            DropColumn("dbo.UserIdentifier", "CreateTime");
        }
    }
}
