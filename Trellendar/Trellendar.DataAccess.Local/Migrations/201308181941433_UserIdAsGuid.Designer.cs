// <auto-generated />
namespace Trellendar.DataAccess.Local.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    public sealed partial class UserIdAsGuid : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(UserIdAsGuid));
        
        string IMigrationMetadata.Id
        {
            get { return "201308181941433_UserIdAsGuid"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}