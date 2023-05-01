using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Nodifier.Blueprint
{
    public interface INodeFactory
    {
        TNode Get<TNode>(IBlueprintGraph graph)
            where TNode : IBlueprintNode;
    }

    internal class BPNodeFactory : INodeFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<(Type, Type), ObjectFactory> _factories = new Dictionary<(Type, Type), ObjectFactory>();

        public BPNodeFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TNode Get<TNode>(IBlueprintGraph graph)
            where TNode : IBlueprintNode
        {
            var editorType = graph.GetType();
            var factoryKey = (typeof(TNode), editorType);

            if (!_factories.TryGetValue(factoryKey, out var factory))
            {
                var nodeFactory = ActivatorUtilities.CreateFactory(typeof(TNode), new[] { editorType });
                _factories.Add(factoryKey, nodeFactory);
                factory = nodeFactory;
            }

            var nodeResult = factory(_serviceProvider, new object[] { graph });
            return (TNode)nodeResult;
        }
    }
}
