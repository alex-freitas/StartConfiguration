using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartupConfiguration.App;
using StartupConfiguration.App.IoC;

namespace StartupConfiguration.ConsoleApp
{
    public class Startup
    {
        public ServiceProvider Configure()
        {
            var services = new ServiceCollection();

            //var configuration = StartupManager.GetRequiredJsonConfiguration("appsettings.json");
            var configuration = StartupManager.GetConfiguration();

            //var appSettings = services.ConfigureAndValidateOptionsDataAnnotations<ConsoleAppSettings>(configuration);
            var appSettings = services.ConfigureSettings<FunctionAppSettings>(configuration);

            services.AddSingleton<IAppService, AppService>();                

            return services.BuildServiceProvider();
        }
    }
}
