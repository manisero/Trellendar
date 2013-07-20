using System;
using Trellendar.Domain.Calendar;

namespace Trellendar.Logic.Domain
{
    public static class TokenExtensions
    {
        public static DateTime GetExpirationTS(this Token token)
        {
            return token.CreationTS.AddSeconds(token.Expires_In);
        }
    }
}
