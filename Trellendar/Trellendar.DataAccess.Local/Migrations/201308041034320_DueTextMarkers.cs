namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DueTextMarkers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPreferences", "DueTextBeginningMarker", c => c.String(maxLength: 10));
            AddColumn("dbo.UserPreferences", "DueTextEndMarker", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPreferences", "DueTextEndMarker");
            DropColumn("dbo.UserPreferences", "DueTextBeginningMarker");
        }
    }
}
