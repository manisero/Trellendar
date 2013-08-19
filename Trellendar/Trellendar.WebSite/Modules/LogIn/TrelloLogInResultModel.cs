using System;
using System.ComponentModel.DataAnnotations;

namespace Trellendar.WebSite.Modules.LogIn
{
    public class TrelloLogInResultModel
    {
        public Guid UserID { get; set; }

        [Required]
        public string AccessToken { get; set; }
    }
}