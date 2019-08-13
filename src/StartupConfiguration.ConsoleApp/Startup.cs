using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartupConfiguration.App;

namespace StartupConfiguration.ConsoleApp
{
    public class Startup
    {
        public ServiceProvider Configure()
        {
            var configuration = new ConfigurationBuilder().GetRequiredJsonConfiguration("appsettings.json");
             
            var services = new ServiceCollection();

            var appSettings = services
                .ConfigureAndValidateOptionsDataAnnotations<ConsoleAppSettings>(configuration);

            services.AddSingleton<IAppService, AppService>();                

            return services.BuildServiceProvider();
        }
    }
}
