namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GoogleDomainIntroduction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "GoogleAccessToken", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Users", "GoogleAccessTokenExpirationTS", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "GoogleRefreshToken", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Users", "BoardID", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Users", "TrelloBoardID");
            DropColumn("dbo.Users", "CalendarAccessToken");
            DropColumn("dbo.Users", "CalendarAccessTokenExpirationTS");
            DropColumn("dbo.Users", "CalendarRefreshToken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "CalendarRefreshToken", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Users", "CalendarAccessTokenExpirationTS", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CalendarAccessToken", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Users", "TrelloBoardID", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Users", "BoardID");
            DropColumn("dbo.Users", "GoogleRefreshToken");
            DropColumn("dbo.Users", "GoogleAccessTokenExpirationTS");
            DropColumn("dbo.Users", "GoogleAccessToken");
        }
    }
}
