using AutoMapper;
using Trellendar.WebSite.AutoMapper.Profiles;

namespace Trellendar.WebSite.AutoMapper
{
    public class AutoMapperBootstrapper
    {
        public void Bootstrap()
        {
            Mapper.AddProfile(new CoreProfile());
            Mapper.AddProfile(new DomainProfile());
            Mapper.AddProfile(new WebSiteProfile());
        }
    }
}