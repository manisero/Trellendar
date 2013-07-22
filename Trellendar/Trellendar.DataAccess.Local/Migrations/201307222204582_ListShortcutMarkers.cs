namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListShortcutMarkers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPreferences", "ListShortcutBeginningMarker", c => c.String(maxLength: 10));
            AddColumn("dbo.UserPreferences", "ListShortcutEndMarker", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPreferences", "ListShortcutEndMarker");
            DropColumn("dbo.UserPreferences", "ListShortcutBeginningMarker");
        }
    }
}
