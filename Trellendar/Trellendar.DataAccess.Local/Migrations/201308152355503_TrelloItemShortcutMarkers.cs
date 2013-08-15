namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrelloItemShortcutMarkers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPreferences", "TrelloItemShortcutBeginningMarker", c => c.String(maxLength: 10));
            AddColumn("dbo.UserPreferences", "TrelloItemShortcutEndMarker", c => c.String(maxLength: 10));
            DropColumn("dbo.UserPreferences", "ListShortcutBeginningMarker");
            DropColumn("dbo.UserPreferences", "ListShortcutEndMarker");
            DropColumn("dbo.UserPreferences", "CheckListShortcutBeginningMarker");
            DropColumn("dbo.UserPreferences", "CheckListShortcutEndMarker");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPreferences", "CheckListShortcutEndMarker", c => c.String(maxLength: 10));
            AddColumn("dbo.UserPreferences", "CheckListShortcutBeginningMarker", c => c.String(maxLength: 10));
            AddColumn("dbo.UserPreferences", "ListShortcutEndMarker", c => c.String(maxLength: 10));
            AddColumn("dbo.UserPreferences", "ListShortcutBeginningMarker", c => c.String(maxLength: 10));
            DropColumn("dbo.UserPreferences", "TrelloItemShortcutEndMarker");
            DropColumn("dbo.UserPreferences", "TrelloItemShortcutBeginningMarker");
        }
    }
}
