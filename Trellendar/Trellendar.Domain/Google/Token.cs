using System;
using Newtonsoft.Json;

namespace Trellendar.Domain.Google
{
    public class Token
    {
        [JsonIgnore]
        public string UserEmail { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonIgnore]
        public DateTime CreationTS { get; set; }
    }
}
