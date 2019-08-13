using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartupConfiguration.App;

[assembly: FunctionsStartup(typeof(StartupConfiguration.FunctionApp.Startup))]
namespace StartupConfiguration.FunctionApp
{
    /* https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection */
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder().GetRequiredJsonConfiguration("local.settings.json");

            var services = builder.Services;

            var appSettings = services
                .ConfigureAndValidateOptions<FunctionAppSettings>(configuration);

            services.AddTransient<IAppService, AppService>();
        }       
    }
}
