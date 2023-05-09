using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Nodifier.Blueprint
{
    public interface INodeFactory
    {
        TNode Get<TNode>(IBlueprintGraph graph)
            where TNode : IBlueprintNode;
        IBlueprintNode Get(Type nodeType, IBlueprintGraph graph);
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
            return (TNode)Get(typeof(TNode), graph);
        }

        public IBlueprintNode Get(Type nodeType, IBlueprintGraph graph)
        {
            if(!typeof(IBlueprintNode).IsAssignableFrom(nodeType))
            {
                throw new ArgumentException($"{nameof(nodeType)} must implement {nameof(IBlueprintNode)}");
            }

            var editorType = graph.GetType();
            var factoryKey = (nodeType, editorType);

            if (!_factories.TryGetValue(factoryKey, out var factory))
            {
                var nodeFactory = ActivatorUtilities.CreateFactory(nodeType, new[] { editorType });
                _factories.Add(factoryKey, nodeFactory);
                factory = nodeFactory;
            }

            var nodeResult = factory(_serviceProvider, new object[] { graph });
            return (IBlueprintNode)nodeResult;
        }
    }
}
