using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens.JWT;
using Newtonsoft.Json;

namespace Trellendar.Core.Serialization._Impl
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize(object item)
        {
            return JsonConvert.SerializeObject(item);
        }

        public object Deserialize(string json, Type itemType)
        {
            return JsonConvert.DeserializeObject(json, itemType);
        }

        public TItem Deserialize<TItem>(string json)
        {
            return JsonConvert.DeserializeObject<TItem>(json);
        }

        public IEnumerable<Claim> DeserializeJWT(string jwt)
        {
            var jwtHandler = new JWTSecurityTokenHandler();

            if (!jwtHandler.CanReadToken(jwt))
            {
                throw new ArgumentException("Cannot read the specified JWT", "jwt");
            }

            var token = jwtHandler.ReadToken(jwt) as JWTSecurityToken;

            if (token == null)
            {
                throw new InvalidOperationException("Cannot read claims from the token");
            }

            return token.Claims;
        }
    }
}