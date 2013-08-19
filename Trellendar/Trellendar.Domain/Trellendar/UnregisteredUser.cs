using System;

namespace Trellendar.Domain.Trellendar
{
    public class UnregisteredUser
    {
        public Guid UnregisteredUserID { get; set; }

        public string Email { get; set; }

        public string GoogleAccessToken { get; set; }

        public DateTime GoogleAccessTokenExpirationTS { get; set; }

        public string GoogleRefreshToken { get; set; }

        public DateTime CreateTS { get; set; }
    }
}
