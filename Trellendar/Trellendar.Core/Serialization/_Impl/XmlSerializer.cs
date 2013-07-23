using System.IO;
using System.Xml;

namespace Trellendar.Core.Serialization._Impl
{
    public class XmlSerializer : IXmlSerializer
    {
        public TOutput Deserialize<TOutput>(string xml)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TOutput));

            return (TOutput)serializer.Deserialize(XmlReader.Create(new StringReader(xml)));
        }
    }
}