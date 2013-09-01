namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCreateTS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CreateTS", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CreateTS");
        }
    }
}
