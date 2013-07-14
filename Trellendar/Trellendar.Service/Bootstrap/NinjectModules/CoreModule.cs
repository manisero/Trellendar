using Ninject.Modules;
using Trellendar.Core.Serialization;
using Trellendar.Core.Serialization._Impl;

namespace Trellendar.Service.Bootstrap.NinjectModules
{
    internal class CoreModule : NinjectModule
    {
        public override void Load()
        {
            // Serialization
            Bind<IJsonSerializer>().To<JsonSerializer>();
        }
    }
}
