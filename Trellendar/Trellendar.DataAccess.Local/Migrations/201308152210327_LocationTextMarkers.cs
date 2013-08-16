namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationTextMarkers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPreferences", "LocationTextBeginningMarker", c => c.String(maxLength: 10));
            AddColumn("dbo.UserPreferences", "LocationTextEndMarker", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPreferences", "LocationTextEndMarker");
            DropColumn("dbo.UserPreferences", "LocationTextBeginningMarker");
        }
    }
}
