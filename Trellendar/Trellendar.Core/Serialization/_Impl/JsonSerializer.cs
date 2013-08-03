using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens.JWT;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Trellendar.Core.Serialization._Impl
{
    public class JsonSerializer : IJsonSerializer
    {
        public class DateConverter : IsoDateTimeConverter
        {
            public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var dateTime = (DateTime?)value;

                if (dateTime.HasValue)
                {
                    writer.WriteValue(dateTime.Value.ToShortDateString());
                }
                else
                {
                    writer.WriteNull();
                }
            }
        }

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