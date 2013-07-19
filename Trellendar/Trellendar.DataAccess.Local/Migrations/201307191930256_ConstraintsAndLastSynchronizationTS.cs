using System.Data.Entity.Migrations;

namespace Trellendar.DataAccess.Local.Migrations
{
    public partial class ConstraintsAndLastSynchronizationTS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastSynchronizationTS", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Users", "TrelloBoardID", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "TrelloAccessToken", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "CalendarID", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "CalendarAccessToken", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "CalendarRefreshToken", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "CalendarRefreshToken", c => c.String());
            AlterColumn("dbo.Users", "CalendarAccessToken", c => c.String());
            AlterColumn("dbo.Users", "CalendarID", c => c.String());
            AlterColumn("dbo.Users", "TrelloAccessToken", c => c.String());
            AlterColumn("dbo.Users", "TrelloBoardID", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            DropColumn("dbo.Users", "LastSynchronizationTS");
        }
    }
}
