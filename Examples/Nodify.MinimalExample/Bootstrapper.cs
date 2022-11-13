﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nodifier;
using System;

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
            services.AddLogging(x => x.SetMinimumLevel(LogLevel.Debug));
            services.AddTransient<IGraphEditor, GraphEditor>();
            services.AddTransient<IGraphEditor, GraphEditor>();
            services.AddSingleton<Func<IGraphEditor>>(x => () => x.GetRequiredService<IGraphEditor>()); // TODO: Create scope for IActionsHistory
        }
    }
}
