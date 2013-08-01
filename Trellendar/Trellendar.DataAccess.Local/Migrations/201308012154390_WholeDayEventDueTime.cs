namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WholeDayEventDueTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPreferences", "WholeDayEventDueTime", c => c.Time());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPreferences", "WholeDayEventDueTime");
        }
    }
}
