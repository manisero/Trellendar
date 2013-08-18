using Ninject.Modules;
using Trellendar.Core.Serialization;
using Trellendar.Core.Serialization._Impl;

namespace Trellendar.WebSite.Ninject.ApplicationModules
{
    internal class CoreModule : NinjectModule
    {
        public override void Load()
        {
            // Serialization
            Bind<IJsonSerializer>().To<JsonSerializer>();
            Bind<IXmlSerializer>().To<XmlSerializer>();
        }
    }
}
