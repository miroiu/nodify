using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Nodifier
{
    public static class NodifierExtensions
    {
        public static void AddNodifier(this IServiceCollection services, Action<NodifierConfiguration>? configure = default)
        {
            var options = new NodifierConfiguration();
            configure?.Invoke(options);

            options.ViewRegistrationStrategy?.Invoke(services, options.Assemblies);

            services.AddSingleton(options);
            services.AddSingleton<XAML.IViewManager, XAML.ViewManager>();
            services.AddSingleton<IViewFactory, DefaultViewFactory>();
            services.AddSingleton<IViewCollection, ViewCollection>();
            services.AddScoped<IActionsHistory, ActionsHistory>();
        }

        public static void UseNodifier(this IServiceProvider provider)
        {
            var viewManager = provider.GetRequiredService<XAML.IViewManager>();
            XAML.View.ViewManager = viewManager;

            var views = provider.GetRequiredService<IViewCollection>();
            var options = provider.GetRequiredService<NodifierConfiguration>();
            options.ViewConfigurationStrategy?.Invoke(views, options.Assemblies);
        }

        public static void Add<TViewModel, TView>(this IViewCollection views) where TView : UIElement
        {
            views.Add(typeof(TViewModel), typeof(TView));
        }

        public static void Record(this IActionsHistory history, Action execute, Action unexecute, string? label = default)
            => history.Record(new DelegateAction(execute, unexecute, label));
    }
}
