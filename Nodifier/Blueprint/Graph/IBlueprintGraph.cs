using System;

namespace Nodifier.Blueprint
{
    public interface IBlueprintGraph
    {
        IGraphWidget Widget { get; }

        IActionsHistory History { get; }

        TNode AddNode<TNode>()
            where TNode : IBlueprintNode;

        IBlueprintNode AddNode(Type nodeType);

        IBlueprintNode AddNode(GraphNodeSnapshot snapshot);
    }
}
