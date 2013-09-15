using AutoMapper;
using Trellendar.Domain.Google;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;

namespace Trellendar.WebSite.AutoMapper.Profiles
{
    public class DomainProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Token, UnregisteredUser>()
                .ForMember(x => x.Email, o => o.MapFrom(x => x.UserEmail))
                .ForMember(x => x.GoogleAccessToken, o => o.MapFrom(x => x.AccessToken))
                .ForMember(x => x.GoogleAccessTokenExpirationTS, o => o.MapFrom(x => x.GetExpirationTS()))
                .ForMember(x => x.GoogleRefreshToken, o => o.MapFrom(x => x.RefreshToken))
                .ForMember(x => x.CreateTS, o => o.Ignore());

            Mapper.CreateMap<UnregisteredUser, User>()
                .ForMember(x => x.CreateTS, o => o.Ignore());

            Mapper.CreateMap<BoardCalendarBondSettings, BoardCalendarBondSettings>()
                .ForMember(x => x.BoardCalendarBondSettingsID, o => o.Ignore())
                .ForMember(x => x.CreateTS, o => o.Ignore())
                .ForMember(x => x.UpdateTS, o => o.Ignore());
        }
    }
}