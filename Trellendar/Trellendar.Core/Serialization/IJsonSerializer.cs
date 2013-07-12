using System;

namespace Trellendar.Core.Serialization
{
    public interface IJsonSerializer
    {
        string Serialize(object item);

        object Deserialize(string json, Type itemType);
        TItem Deserialize<TItem>(string json);
    }
}
