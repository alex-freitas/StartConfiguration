using Microsoft.Extensions.DependencyInjection;
using StartupConfiguration.App;
using System;

namespace StartupConfiguration.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Startup()
                .Configure()
                .GetRequiredService<IAppService>()
                .Run();
        }
    }
}
