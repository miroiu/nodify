namespace Nodifier.Blueprint
{
    public class BPNode<TGraph, TWidget, TSnapshot> : IBlueprintNode, IMemento<TSnapshot>
        where TGraph : class, IBlueprintGraph
        where TWidget : class, IGraphElement
        where TSnapshot : INodeSnapshot
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
            snapshot.IsSelected = Widget.IsSelected;
        }

        public virtual void RestoreSnapshot(TSnapshot snapshot)
        {
            Widget.Location = new System.Windows.Point(snapshot.X, snapshot.Y);
            Widget.IsSelected = Widget.IsSelected;
        }
    }

    public class BPNode<TSnapshot> : BPNode<BPGraph, INodeWidget, TSnapshot>
        where TSnapshot : class, INodeSnapshot
    {
        public BPNode(BPGraph graph) : base(graph, new NodeWidget(graph.Widget))
        {
        }

        public FlowInput AddFlowInput(string? title = default)
        {
            var input = new FlowInput(Widget) { Title = title };
            Widget.AddInput(input);
            return input;
        }

        public FlowOutput AddFlowOutput(string? title = default)
        {
            var output = new FlowOutput(Widget) { Title = title };
            Widget.AddOutput(output);
            return output;
        }

        public ValueInput<T> AddValueInput<T>(string? title = default, T value = default!)
        {
            var input = new ValueInput<T>(Widget)
            {
                Title = title,
                Value = value
            };

            Widget.AddInput(input);
            return input;
        }

        public ValueOutput<T> AddValueOutput<T>(string? title = default, T value = default!)
        {
            var output = new ValueOutput<T>(Widget)
            {
                Title = title,
                Value = value
            };

            Widget.AddOutput(output);
            return output;
        }
    }

    public class BPNode : BPNode<BPGraph, INodeWidget, BPNodeSnapshot>
    {
        public BPNode(BPGraph graph) : base(graph, new NodeWidget(graph.Widget))
        {
        }
    }
}
