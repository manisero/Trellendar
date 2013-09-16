using AutoMapper;
using Trellendar.WebSite.AutoMapper.Converters;

namespace Trellendar.WebSite.AutoMapper.Profiles
{
    public class CoreProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<string, string>().ConvertUsing<StringToStringConverter>();
        }
    }
}