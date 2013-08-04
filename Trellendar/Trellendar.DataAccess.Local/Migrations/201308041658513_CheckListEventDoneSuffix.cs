namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckListEventDoneSuffix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPreferences", "CheckListEventDoneSuffix", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPreferences", "CheckListEventDoneSuffix");
        }
    }
}
