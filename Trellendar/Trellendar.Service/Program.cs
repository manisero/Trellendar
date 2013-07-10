using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace Trellendar.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if !DEBUG
            ServiceBase.Run(new ServiceBase[] { new Trellendar() });
#else
            typeof(Trellendar).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic)
                              .Invoke(new Trellendar(), new object[] { null });

            Thread.Sleep(Timeout.Infinite);
#endif
        }
    }
}
