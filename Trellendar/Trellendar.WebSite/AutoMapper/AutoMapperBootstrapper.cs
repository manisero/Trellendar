using AutoMapper;
using Trellendar.Domain.Trellendar;

namespace Trellendar.WebSite.AutoMapper
{
    public class AutoMapperBootstrapper
    {
        public void Bootstrap()
        {
            // BondsSettings Module
            Mapper.CreateMap<BoardCalendarBondSettings, Modules.BondSettings.Models.IndexModel>();
            Mapper.CreateMap<Modules.BondSettings.Models.IndexModel, BoardCalendarBondSettings>();
        }
    }
}