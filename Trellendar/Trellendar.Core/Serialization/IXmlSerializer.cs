namespace Trellendar.Core.Serialization
{
    public interface IXmlSerializer
    {
        TOutput Deserialize<TOutput>(string xml);
    }
}
