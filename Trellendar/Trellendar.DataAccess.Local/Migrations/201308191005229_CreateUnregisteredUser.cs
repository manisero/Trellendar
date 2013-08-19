namespace Trellendar.DataAccess.Local.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUnregisteredUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UnregisteredUsers",
                c => new
                    {
                        UnregisteredUserID = c.Guid(nullable: false, defaultValueSql: "newid()"),
                        Email = c.String(nullable: false, maxLength: 256),
                        GoogleAccessToken = c.String(nullable: false, maxLength: 100),
                        GoogleAccessTokenExpirationTS = c.DateTime(nullable: false),
                        GoogleRefreshToken = c.String(nullable: false, maxLength: 100),
                        CreateTS = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UnregisteredUserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UnregisteredUsers");
        }
    }
}
