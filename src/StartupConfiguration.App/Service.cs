using System;
using System.Diagnostics;

namespace StartupConfiguration.App
{
    public interface IAppService
    {
        void Run();
    }

    public class AppService : IAppService
    {

        public void Run()
        {
            Debug.WriteLine($"AppService.Running from {AppDomain.CurrentDomain.FriendlyName}" );
        }
    }
}
