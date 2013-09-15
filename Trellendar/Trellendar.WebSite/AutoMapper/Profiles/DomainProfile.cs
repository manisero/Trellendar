using AutoMapper;
using Trellendar.Domain.Trellendar;

namespace Trellendar.WebSite.AutoMapper.Profiles
{
    public class DomainProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BoardCalendarBondSettings, BoardCalendarBondSettings>()
                .ForMember(x => x.BoardCalendarBondSettingsID, _ => _.Ignore())
                .ForMember(x => x.CreateTS, _ => _.Ignore())
                .ForMember(x => x.UpdateTS, _ => _.Ignore());
        }
    }
}