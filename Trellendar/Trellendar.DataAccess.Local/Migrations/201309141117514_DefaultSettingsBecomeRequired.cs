namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefaultSettingsBecomeRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID", "dbo.BoardCalendarBondSettings");
            DropIndex("dbo.Users", new[] { "DefaultBondSettings_BoardCalendarBondSettingsID" });
            AlterColumn("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID", c => c.Int(nullable: false));
            AddForeignKey("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID", "dbo.BoardCalendarBondSettings", "BoardCalendarBondSettingsID", cascadeDelete: true);
            CreateIndex("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "DefaultBondSettings_BoardCalendarBondSettingsID" });
            DropForeignKey("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID", "dbo.BoardCalendarBondSettings");
            AlterColumn("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID", c => c.Int());
            CreateIndex("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID");
            AddForeignKey("dbo.Users", "DefaultBondSettings_BoardCalendarBondSettingsID", "dbo.BoardCalendarBondSettings", "BoardCalendarBondSettingsID");
        }
    }
}
