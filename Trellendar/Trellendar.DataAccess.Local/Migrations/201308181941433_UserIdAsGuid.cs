namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdAsGuid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPreferences", "UserID", "dbo.Users");
            DropIndex("dbo.UserPreferences", new[] { "UserID" });
            DropPrimaryKey("dbo.UserPreferences");
            DropPrimaryKey("dbo.Users");

            DropColumn("dbo.Users", "UserID");
            AddColumn("dbo.Users", "UserID", c => c.Guid(nullable: false, defaultValueSql: "newid()"));

            DropColumn("dbo.UserPreferences", "UserID");
            AddColumn("dbo.UserPreferences", "UserID", c => c.Guid(nullable: false));

            AddPrimaryKey("dbo.Users", "UserID");
            AddPrimaryKey("dbo.UserPreferences", "UserID");
            AddForeignKey("dbo.UserPreferences", "UserID", "dbo.Users", "UserID");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserPreferences", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "UserID", c => c.Int(nullable: false, identity: true));
        }
    }
}
