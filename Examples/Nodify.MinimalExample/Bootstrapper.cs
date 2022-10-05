using Microsoft.Extensions.DependencyInjection;
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
            serviceProvider.UseNodifier();

            Application = serviceProvider.GetRequiredService<MinimalApp>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddNodifier();
            services.AddSingleton<MinimalApp>();
        }
    }
}
