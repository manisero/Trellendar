using System;
using System.ComponentModel.DataAnnotations;

namespace Trellendar.WebSite.Modules.LogIn.Models
{
    public class TrelloLogInResultModel
    {
        public Guid UserID { get; set; }

        [Required]
        public string AccessToken { get; set; }
    }
}