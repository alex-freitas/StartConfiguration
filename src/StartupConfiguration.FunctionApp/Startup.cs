using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartupConfiguration.App;
using StartupConfiguration.App.IoC;

[assembly: FunctionsStartup(typeof(StartupConfiguration.FunctionApp.Startup))]
namespace StartupConfiguration.FunctionApp
{
    /* https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection */
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;

            var configuration = StartupManager.GetConfiguration();

            var appSettings = services.ConfigureSettings<FunctionAppSettings>(configuration);

            services.AddTransient<IAppService, AppService>();
        }        
    }
}
