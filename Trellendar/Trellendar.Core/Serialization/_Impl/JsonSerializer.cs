using System;
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
    }
}