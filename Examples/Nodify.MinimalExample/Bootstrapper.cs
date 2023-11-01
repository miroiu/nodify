using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nodifier;

namespace Nodify.MinimalExample
{
    public class Bootstrapper
    {
        public MinimalApp Application { get; }

        public Bootstrapper()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.UseBlueprints();

            Application = serviceProvider.GetRequiredService<MinimalApp>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddBlueprints();
            services.AddSingleton<MinimalApp>();
            services.AddLogging(x => x.SetMinimumLevel(LogLevel.Debug));
        }
    }
}
