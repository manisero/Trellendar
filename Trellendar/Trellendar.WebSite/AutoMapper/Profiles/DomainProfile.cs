using AutoMapper;
using Trellendar.Domain.Google;
using Trellendar.Domain.Trellendar;
using Trellendar.Logic.Domain;
using Trellendar.Core.Extensions.AutoMapper;

namespace Trellendar.WebSite.AutoMapper.Profiles
{
    public class DomainProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Token, UnregisteredUser>()
                .Map(x => x.UserEmail, x => x.Email)
                .Map(x => x.AccessToken, x => x.GoogleAccessToken)
                .Map(x => x.GetExpirationTS(), x => x.GoogleAccessTokenExpirationTS)
                .Map(x => x.RefreshToken, x => x.GoogleRefreshToken)
                .Ignore(x => x.CreateTS);

            CreateMap<UnregisteredUser, User>()
                .Ignore(x => x.CreateTS);

            CreateMap<BoardCalendarBondSettings, BoardCalendarBondSettings>()
                .Ignore(x => x.BoardCalendarBondSettingsID)
                .Ignore(x => x.CreateTS)
                .Ignore(x => x.UpdateTS);
        }
    }
}