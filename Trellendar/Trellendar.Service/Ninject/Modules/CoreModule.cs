using Ninject.Modules;
using Trellendar.Core.Serialization;
using Trellendar.Core.Serialization._Impl;

namespace Trellendar.Service.Ninject.Modules
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
