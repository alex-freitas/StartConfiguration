using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StartupConfiguration.App;
using System;
using System.ComponentModel.DataAnnotations;

namespace StartupConfiguration.App.IoC
{
    public static class StartupManager
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
        }

        public static IConfigurationRoot GetRequiredJsonConfiguration(string path)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile(path, false, true)
                .Build();
        }

        #region Extensions


        public static T ConfigureAndValidateOptionsDataAnnotations<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            var configurationSection = configuration.GetSection("Values");
            var optionsBuilder = services
                .AddOptions<T>()
                .Bind(configurationSection);

            var options = optionsBuilder.ValidateOptionsDataAnnotations<T>(services);

            return options;
        }

        public static T ConfigureAndValidateOptions<T>(this IServiceCollection services, IConfiguration configuration) where T : class
        {
            services.Configure<T>(configuration);

            return configuration.GetValid<T>();           
        }

        public static T ConfigureSettings<T>(this IServiceCollection services, IConfiguration configuration) where T : class 
        {
            services.Configure<T>(configuration);

            return configuration.Get<T>(); 
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

        #endregion

    }
}
