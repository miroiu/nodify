namespace Nodifier.Blueprint
{
    public class BPNode<TGraph, TWidget, TSnapshot> : IBlueprintNode, IMemento<TSnapshot>, INodeMemento
        where TGraph : class, IBlueprintGraph
        where TWidget : class, IGraphElement
        where TSnapshot : INodeSnapshot, new()
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

        public TSnapshot CreateSnapshot()
        {
            var snapshot = new TSnapshot
            {
                X = Widget.Location.X,
                Y = Widget.Location.Y,
                IsSelected = Widget.IsSelected
            };

            OnSnapshotCreating(snapshot);
            return snapshot;
        }

        protected virtual void OnSnapshotCreating(TSnapshot snapshot)
        {
        }

        public virtual void RestoreSnapshot(TSnapshot snapshot)
        {
            using (History.Batch(nameof(RestoreSnapshot)))
            {
                Widget.Location = new System.Windows.Point(snapshot.X, snapshot.Y);
                Widget.IsSelected = snapshot.IsSelected;
            }
        }

        INodeSnapshot INodeMemento.CreateSnapshot()
        {
            return CreateSnapshot();
        }

        void INodeMemento.RestoreSnapshot(INodeSnapshot snapshot)
        {
            RestoreSnapshot((TSnapshot)snapshot);
        }
    }

    public class BPNode<TSnapshot> : BPNode<IBlueprintGraph, INodeWidget, TSnapshot>
        where TSnapshot : INodeSnapshot, new()
    {
        public BPNode(IBlueprintGraph graph) : base(graph, new NodeWidget(graph.Widget))
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

    public class BPNode : BPNode<IBlueprintGraph, INodeWidget, BPNodeSnapshot>
    {
        public BPNode(IBlueprintGraph graph) : base(graph, new NodeWidget(graph.Widget))
        {
        }
    }
}
