using AutoMapper;
using Trellendar.Domain.Calendar;
using Trellendar.Domain.Trellendar;
using Trellendar.Domain.Trello;
using Trellendar.Core.Extensions.AutoMapper;

namespace Trellendar.WebSite.AutoMapper.Profiles
{
    public class WebSiteProfile : Profile
    {
        protected override void Configure()
        {
            // Bonds Module
            CreateMap<BoardCalendarBond, Modules.Bonds.Models.BoardCalendarBondModel>();
            CreateMap<Board, Modules.Bonds.Models.BoardModel>();
            CreateMap<Calendar, Modules.Bonds.Models.CalendarModel>()
                .Map(x => x.Summary, x => x.Name);

            // BondsSettings Module
            CreateMap<BoardCalendarBondSettings, Modules.BondSettings.Models.IndexModel>();
            CreateMap<Modules.BondSettings.Models.IndexModel, BoardCalendarBondSettings>();
        }
    }
}