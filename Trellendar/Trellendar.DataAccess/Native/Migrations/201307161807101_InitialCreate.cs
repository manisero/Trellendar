namespace Trellendar.DataAccess.Native
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        TrelloBoardID = c.String(),
                        TrelloAccessToken = c.String(),
                        CalendarID = c.String(),
                        CalendarAccessToken = c.String(),
                        CalendarAccessTokenExpirationTS = c.DateTime(nullable: false),
                        CalendarRefreshToken = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
