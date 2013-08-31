namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoardCalendarBondIntroduction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoardCalendarBonds",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        BoardID = c.String(nullable: false, maxLength: 100),
                        CalendarID = c.String(nullable: false, maxLength: 100),
                        CalendarTimeZone = c.String(maxLength: 50),
                        CreateTS = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.BoardID, t.CalendarID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BoardCalendarBonds", new[] { "UserID" });
            DropForeignKey("dbo.BoardCalendarBonds", "UserID", "dbo.Users");
            DropTable("dbo.BoardCalendarBonds");
        }
    }
}
