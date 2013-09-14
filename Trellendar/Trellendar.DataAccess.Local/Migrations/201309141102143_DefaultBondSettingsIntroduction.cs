namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultBondSettingsIntroduction : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BoardCalendarBondSettings", new[] { "UserID", "BoardID", "CalendarID" }, "dbo.BoardCalendarBonds");
            DropIndex("dbo.BoardCalendarBondSettings", new[] { "UserID", "BoardID", "CalendarID" });
            AddColumn("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID", c => c.Int());
            AddColumn("dbo.BoardCalendarBonds", "Settings_BoardCalendarBondSettingsID", c => c.Int());
            AddColumn("dbo.BoardCalendarBondSettings", "BoardCalendarBondSettingsID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.BoardCalendarBondSettings", new[] { "UserID", "BoardID", "CalendarID" });
            AddPrimaryKey("dbo.BoardCalendarBondSettings", "BoardCalendarBondSettingsID");
            AddForeignKey("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID", "dbo.BoardCalendarBondSettings", "BoardCalendarBondSettingsID");
            AddForeignKey("dbo.BoardCalendarBonds", "Settings_BoardCalendarBondSettingsID", "dbo.BoardCalendarBondSettings", "BoardCalendarBondSettingsID");
            CreateIndex("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID");
            CreateIndex("dbo.BoardCalendarBonds", "Settings_BoardCalendarBondSettingsID");
            DropColumn("dbo.BoardCalendarBondSettings", "UserID");
            DropColumn("dbo.BoardCalendarBondSettings", "BoardID");
            DropColumn("dbo.BoardCalendarBondSettings", "CalendarID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BoardCalendarBondSettings", "CalendarID", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.BoardCalendarBondSettings", "BoardID", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.BoardCalendarBondSettings", "UserID", c => c.Guid(nullable: false));
            DropIndex("dbo.BoardCalendarBonds", new[] { "Settings_BoardCalendarBondSettingsID" });
            DropIndex("dbo.Users", new[] { "DefaultBondSettings_BoardCalendarBondSettingsID" });
            DropForeignKey("dbo.BoardCalendarBonds", "Settings_BoardCalendarBondSettingsID", "dbo.BoardCalendarBondSettings");
            DropForeignKey("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID", "dbo.BoardCalendarBondSettings");
            DropPrimaryKey("dbo.BoardCalendarBondSettings", new[] { "BoardCalendarBondSettingsID" });
            AddPrimaryKey("dbo.BoardCalendarBondSettings", new[] { "UserID", "BoardID", "CalendarID" });
            DropColumn("dbo.BoardCalendarBondSettings", "BoardCalendarBondSettingsID");
            DropColumn("dbo.BoardCalendarBonds", "Settings_BoardCalendarBondSettingsID");
            DropColumn("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID");
            CreateIndex("dbo.BoardCalendarBondSettings", new[] { "UserID", "BoardID", "CalendarID" });
            AddForeignKey("dbo.BoardCalendarBondSettings", new[] { "UserID", "BoardID", "CalendarID" }, "dbo.BoardCalendarBonds", new[] { "UserID", "BoardID", "CalendarID" });
        }
    }
}
