using System;

namespace Trellendar.Domain.Calendar
{
    public class Token
    {
        public string Access_Token { get; set; }

        public string Id_Token { get; set; }

        public int Expires_In { get; set; }

        public string Refresh_Token { get; set; }

        public DateTime CreationTS { get; set; }

        public DateTime ExpirationTS
        {
            get { return CreationTS.AddSeconds(Expires_In); }
        }
    }
}
