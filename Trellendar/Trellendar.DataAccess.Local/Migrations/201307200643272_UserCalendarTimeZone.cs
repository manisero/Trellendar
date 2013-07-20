namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCalendarTimeZone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CalendarTimeZone", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CalendarTimeZone");
        }
    }
}
