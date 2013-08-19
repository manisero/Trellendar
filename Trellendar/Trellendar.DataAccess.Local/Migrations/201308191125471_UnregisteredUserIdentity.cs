namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnregisteredUserIdentity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UnregisteredUsers", "UnregisteredUserID", c => c.Guid(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UnregisteredUsers", "UnregisteredUserID", c => c.Guid(nullable: false));
        }
    }
}
