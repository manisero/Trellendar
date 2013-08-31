namespace Trellendar.DataAccess.Remote.Google
{
    public class GoogleClient : RestClientBase
    {
        public GoogleClient() : base("https://accounts.google.com/o/oauth2/")
        {
        }
    }
}