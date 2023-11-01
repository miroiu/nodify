using Microsoft.Extensions.DependencyInjection;
using System;

namespace Nodifier.Blueprint
{
    public interface IGraphFactory
    {
        TGraph Get<TGraph>() where TGraph : IBlueprintGraph;
    }

    internal class BPGraphFactory : IGraphFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public BPGraphFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TGraph Get<TGraph>() where TGraph : IBlueprintGraph
        {
            return _serviceProvider.GetRequiredService<TGraph>();
        }
    }
}
