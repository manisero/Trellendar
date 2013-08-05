namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckListEventNameTemplate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPreferences", "CheckListEventNameTemplate", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPreferences", "CheckListEventNameTemplate");
        }
    }
}
