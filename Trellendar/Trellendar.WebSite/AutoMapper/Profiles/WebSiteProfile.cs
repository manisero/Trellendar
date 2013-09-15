using AutoMapper;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;

namespace Trellendar.WebSite.AutoMapper.Profiles
{
    public class WebSiteProfile : Profile
    {
        protected override void Configure()
        {
            // Bonds Module
            Mapper.CreateMap<BoardCalendarBond, Modules.Bonds.Models.BoardCalendarBondModel>();
            Mapper.CreateMap<Board, Modules.Bonds.Models.BoardModel>();
            Mapper.CreateMap<Calendar, Modules.Bonds.Models.CalendarModel>()
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Summary));

            // BondsSettings Module
            Mapper.CreateMap<BoardCalendarBondSettings, Modules.BondSettings.Models.IndexModel>();
            Mapper.CreateMap<Modules.BondSettings.Models.IndexModel, BoardCalendarBondSettings>();
        }
    }
}