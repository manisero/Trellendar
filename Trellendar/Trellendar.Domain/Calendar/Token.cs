namespace Trellendar.Domain.Calendar
{
    public class Token
    {
        public string Access_Token { get; set; }

        public int Expires_In { get; set; }

        public string Refresh_Token { get; set; }
    }
}
