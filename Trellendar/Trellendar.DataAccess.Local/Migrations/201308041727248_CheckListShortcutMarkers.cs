namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckListShortcutMarkers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPreferences", "CheckListShortcutBeginningMarker", c => c.String(maxLength: 10));
            AddColumn("dbo.UserPreferences", "CheckListShortcutEndMarker", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPreferences", "CheckListShortcutEndMarker");
            DropColumn("dbo.UserPreferences", "CheckListShortcutBeginningMarker");
        }
    }
}
