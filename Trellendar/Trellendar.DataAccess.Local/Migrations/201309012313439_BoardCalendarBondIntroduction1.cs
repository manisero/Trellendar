namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoardCalendarBondIntroduction1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoardCalendarBondSettings",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        BoardID = c.String(nullable: false, maxLength: 100),
                        CalendarID = c.String(nullable: false, maxLength: 100),
                        TrelloItemShortcutBeginningMarker = c.String(maxLength: 10),
                        TrelloItemShortcutEndMarker = c.String(maxLength: 10),
                        CardEventNameTemplate = c.String(maxLength: 50),
                        CheckListEventNameTemplate = c.String(maxLength: 50),
                        CheckListEventDoneSuffix = c.String(maxLength: 50),
                        WholeDayEventDueTime = c.Time(),
                        DueTextBeginningMarker = c.String(maxLength: 10),
                        DueTextEndMarker = c.String(maxLength: 10),
                        LocationTextBeginningMarker = c.String(maxLength: 10),
                        LocationTextEndMarker = c.String(maxLength: 10),
                        CreateTS = c.DateTime(nullable: false),
                        UpdateTS = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.BoardID, t.CalendarID })
                .ForeignKey("dbo.BoardCalendarBonds", t => new { t.UserID, t.BoardID, t.CalendarID })
                .Index(t => new { t.UserID, t.BoardID, t.CalendarID });
            
            AddColumn("dbo.BoardCalendarBonds", "LastSynchronizationTS", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "BoardID");
            DropColumn("dbo.Users", "CalendarID");
            DropColumn("dbo.Users", "CalendarTimeZone");
            DropColumn("dbo.Users", "LastSynchronizationTS");
            DropTable("dbo.UserPreferences");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPreferences",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        TrelloItemShortcutBeginningMarker = c.String(maxLength: 10),
                        TrelloItemShortcutEndMarker = c.String(maxLength: 10),
                        CardEventNameTemplate = c.String(maxLength: 50),
                        CheckListEventNameTemplate = c.String(maxLength: 50),
                        CheckListEventDoneSuffix = c.String(maxLength: 50),
                        WholeDayEventDueTime = c.Time(),
                        DueTextBeginningMarker = c.String(maxLength: 10),
                        DueTextEndMarker = c.String(maxLength: 10),
                        LocationTextBeginningMarker = c.String(maxLength: 10),
                        LocationTextEndMarker = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.Users", "LastSynchronizationTS", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CalendarTimeZone", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "CalendarID", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Users", "BoardID", c => c.String(nullable: false, maxLength: 100));
            DropIndex("dbo.BoardCalendarBondSettings", new[] { "UserID", "BoardID", "CalendarID" });
            DropForeignKey("dbo.BoardCalendarBondSettings", new[] { "UserID", "BoardID", "CalendarID" }, "dbo.BoardCalendarBonds");
            DropColumn("dbo.BoardCalendarBonds", "LastSynchronizationTS");
            DropTable("dbo.BoardCalendarBondSettings");
            CreateIndex("dbo.UserPreferences", "UserID");
            AddForeignKey("dbo.UserPreferences", "UserID", "dbo.Users", "UserID");
        }
    }
}
