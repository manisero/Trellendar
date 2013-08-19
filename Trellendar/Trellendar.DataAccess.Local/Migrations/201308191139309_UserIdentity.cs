namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdentity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserID", c => c.Guid(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UserID", c => c.Guid(nullable: false));
        }
    }
}
