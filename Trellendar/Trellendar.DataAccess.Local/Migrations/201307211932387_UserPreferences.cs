namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPreferences : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPreferences",
                c => new
                    {
                        UserPreferencesID = c.Int(nullable: false, identity: true),
                        CardEventNameTemplate = c.String(maxLength: 50),
                        User_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserPreferencesID)
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .Index(t => t.User_UserID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserPreferences", new[] { "User_UserID" });
            DropForeignKey("dbo.UserPreferences", "User_UserID", "dbo.Users");
            DropTable("dbo.UserPreferences");
        }
    }
}
