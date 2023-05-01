namespace Nodifier.Blueprint
{
    public interface IBlueprintNode
    {
        IGraphElement Widget { get; }
        IBlueprintGraph Graph { get; }

        IActionsHistory History { get; }
    }

    public interface IMemento<TSnapshot>
    {
        void SaveSnapshot(TSnapshot snapshot);
        void RestoreSnapshot(TSnapshot snapshot);
    }

    public interface ILocationSnapshot
    { 
        double X { get; set; }
        double Y { get; set; }
    }

    public class BPNodeSnapshot : ILocationSnapshot
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class BPNode<TGraph, TWidget, TSnapshot> : IBlueprintNode, IMemento<TSnapshot>
        where TGraph : class, IBlueprintGraph
        where TWidget : class, IGraphElement
        where TSnapshot : class, ILocationSnapshot
    {
        IGraphElement IBlueprintNode.Widget => Widget;
        IBlueprintGraph IBlueprintNode.Graph => Graph;

        public TWidget Widget { get; }
        public TGraph Graph { get; }

        public IActionsHistory History => Graph.History;

        public BPNode(TGraph graph, TWidget widget)
        {
            Graph = graph;
            Widget = widget;
        }

        public virtual void SaveSnapshot(TSnapshot snapshot)
        {
            snapshot.X = Widget.Location.X;
            snapshot.Y = Widget.Location.Y;
        }

        public virtual void RestoreSnapshot(TSnapshot snapshot)
        {
            Widget.Location = new System.Windows.Point(snapshot.X, snapshot.Y);
        }
    }

    public class BPNode<TSnapshot> : BPNode<BPGraph, INodeWidget, TSnapshot>
        where TSnapshot : class, ILocationSnapshot
    {
        public BPNode(BPGraph graph) : base(graph, new NodeWidget(graph.Widget))
        {
        }
    }

    public class BPNode : BPNode<BPGraph, INodeWidget, BPNodeSnapshot>
    {
        public BPNode(BPGraph graph) : base(graph, new NodeWidget(graph.Widget))
        {
        }
    }
}
