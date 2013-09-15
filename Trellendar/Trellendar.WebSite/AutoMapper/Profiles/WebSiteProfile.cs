using AutoMapper;
using Trellendar.Domain.Trellendar;

namespace Trellendar.WebSite.AutoMapper.Profiles
{
    public class WebSiteProfile : Profile
    {
        protected override void Configure()
        {
            // BondsSettings Module
            Mapper.CreateMap<BoardCalendarBondSettings, Modules.BondSettings.Models.IndexModel>();
            Mapper.CreateMap<Modules.BondSettings.Models.IndexModel, BoardCalendarBondSettings>();
        }
    }
}