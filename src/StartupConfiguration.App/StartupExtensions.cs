using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StartupConfiguration.App;
using System;
using System.ComponentModel.DataAnnotations;

namespace StartupConfiguration.App 
{
    public static class StartupExtensions
    { 
        public static IConfigurationRoot GetRequiredJsonConfiguration(this IConfigurationBuilder configurationBuilder, string path)
        {
            return configurationBuilder
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile(path, false, true)
                .Build();
        }
        
        public static T ConfigureAndValidateOptionsDataAnnotations<T>(this IServiceCollection services, IConfigurationRoot configuration) where T : class
        {
            var configurationSection = configuration.GetSection("Values");
            var optionsBuilder = services
                .AddOptions<T>()
                .Bind(configurationSection);

            var options = optionsBuilder.ValidateOptionsDataAnnotations<T>(services);

            return options;
        }

        public static T ConfigureAndValidateOptions<T>(this IServiceCollection services, IConfigurationRoot configuration) where T : class
        {
            var configurationSection = configuration.GetSection("Values");
            var options = configurationSection.GetValid<T>();

            var optionsBuilder = services
                .AddOptions<T>()
                .Bind(configurationSection);

            return options;
        }

        private static T ValidateOptionsDataAnnotations<T>(this OptionsBuilder<T> optionsBuilder, IServiceCollection services) where T : class
        {
            /* https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/configuration/options?view=aspnetcore-2.2 */
            /* Not working on AzureFunctions v2 1.0.29 */
            optionsBuilder.ValidateDataAnnotations();

            var serviceProvider = services.BuildServiceProvider();

            var options = serviceProvider
                .GetRequiredService<IOptionsMonitor<T>>()
                .CurrentValue;

            return options;
        }

        
        private static T GetValid<T>(this IConfiguration configuration)
        {
            /* https://stackoverflow.com/a/54989674 */
            var obj = configuration.Get<T>();
            Validator.ValidateObject(obj, new ValidationContext(obj), true);
            return obj;
        }
    }
}
