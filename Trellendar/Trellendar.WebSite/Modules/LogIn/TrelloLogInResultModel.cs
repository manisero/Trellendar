using System;

namespace Trellendar.WebSite.Modules.LogIn
{
    public class TrelloLogInResultModel
    {
        public Guid UserID { get; set; }

        public string AccessToken { get; set; }
    }
}