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
                        UserID = c.Int(nullable: false),
                        CardEventNameTemplate = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserPreferences", new[] { "UserID" });
            DropForeignKey("dbo.UserPreferences", "UserID", "dbo.Users");
            DropTable("dbo.UserPreferences");
        }
    }
}
