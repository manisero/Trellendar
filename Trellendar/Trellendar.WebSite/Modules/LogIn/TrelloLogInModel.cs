using System;

namespace Trellendar.WebSite.Modules.LogIn
{
    public class TrelloLogInModel
    {
        public Guid UserID { get; set; }

        public string AccessToken { get; set; }
    }
}