using AutoMapper;
using Trellendar.Core.Extensions;

namespace Trellendar.WebSite.AutoMapper.Converters
{
    public class StringToStringConverter : ITypeConverter<string, string>
    {
        public string Convert(ResolutionContext context)
        {
            return ((string)context.SourceValue).GetValueOrDefault();
        }
    }
}