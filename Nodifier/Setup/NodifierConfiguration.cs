using Microsoft.Extensions.DependencyInjection;
using Nodifier.Views;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nodifier
{
    public delegate void ViewRegistrationStrategy(IServiceCollection services, IReadOnlyCollection<Assembly> assemblies);
    public delegate void ViewConfigurationStrategy(IViewCollection views, IReadOnlyCollection<Assembly> assemblies);

    public class NodifierConfiguration
    {
        public IReadOnlyCollection<Assembly> Assemblies { get; set; }
        public ViewConfigurationStrategy? ViewConfigurationStrategy { get; set; }
        public ViewRegistrationStrategy? ViewRegistrationStrategy { get; set; }

        public static readonly ViewConfigurationStrategy AutomaticViewConfiguration = (views, assemblies) =>
        {
            assemblies
            .ToList()
            .ForEach(x => x.GetExportedTypes()
                .Where(type => !type.IsInterface && type.GetInterfaces()
                    .Any(i => i.IsGenericType && typeof(XAML.IViewFor<>).IsAssignableFrom(i.GetGenericTypeDefinition())))
                .Select(type => new
                {
                    Type = type,
                    Interfaces = type.GetInterfaces().Where(i => i.IsGenericType && typeof(XAML.IViewFor<>).IsAssignableFrom(i.GetGenericTypeDefinition())).ToList()
                })
                .ToList()
                .ForEach(x => x.Interfaces.ForEach(i => views.Add(i.GetGenericArguments()[0], x.Type))));
        };

        public static readonly ViewConfigurationStrategy ManualViewConfiguration = (views, assemblies) =>
        {
            views.Add<IGraphWidget, GraphView>();
            views.Add<INodeWidget, GraphNodeView>();
            views.Add<IRelayNodeWidget, RelayNodeView>();
            views.Add<IConnection, ConnectionView>();
            views.Add<IPendingConnection, PendingConnectionView>();
            views.Add<IConnector, ConnectorView>();
            views.Add<string, TextView>();
        };

        public static readonly ViewRegistrationStrategy AutomaticViewRegistration = (services, assemblies) => assemblies
                .ToList()
                .ForEach(x => x.GetExportedTypes()
                    .Where(type => !type.IsInterface && type.GetInterfaces()
                        .Any(i => i.IsGenericType && typeof(XAML.IViewFor<>).IsAssignableFrom(i.GetGenericTypeDefinition())))
                    .ToList()
                    .ForEach(x => services.AddTransient(x)));

        public static readonly ViewRegistrationStrategy ManualViewRegistration = (services, assemblies) =>
        {
            services.AddTransient<GraphView>();
            services.AddTransient<GraphNodeView>();
            services.AddTransient<RelayNodeView>();
            services.AddTransient<ConnectionView>();
            services.AddTransient<PendingConnectionView>();
            services.AddTransient<TextView>();
            services.AddTransient<ConnectorView>();
        };

        public NodifierConfiguration()
        {
            var entry = Assembly.GetEntryAssembly();
            var list = new List<Assembly> { typeof(XAML.ViewManager).Assembly };
            if (entry != null)
            {
                list.Add(entry);
            }

            Assemblies = list;

            ViewConfigurationStrategy = AutomaticViewConfiguration;
            ViewRegistrationStrategy = AutomaticViewRegistration;
        }
    }
}
