namespace Nodifier.Blueprint
{
    public interface IGraphSnapshot
    {
        double X { get; set; }
        double Y { get; set; }
        double Zoom { get; set; }
    }

    public interface IBlueprintGraph
    {
        IGraphWidget Widget { get; }

        IActionsHistory History { get; }

        TNode AddNode<TNode>()
            where TNode : IBlueprintNode;
    }

    public class BPGraph<TEditorWidget> : IBlueprintGraph, IMemento<IGraphSnapshot>
        where TEditorWidget : class, IGraphWidget
    {
        private readonly INodeFactory _nodeFactory;

        IGraphWidget IBlueprintGraph.Widget => Widget;

        public TEditorWidget Widget { get; }

        public IActionsHistory History => Widget.History;

        public BPGraph(TEditorWidget editor, INodeFactory nodeFactory)
        {
            Widget = editor;
            _nodeFactory = nodeFactory;
        }

        public TNode AddNode<TNode>()
            where TNode : IBlueprintNode
        {
            var node = _nodeFactory.Get<TNode>(this);
            Widget.AddElement(node.Widget);
            return node;
        }

        // TODO: Save elements + connections
        public void SaveSnapshot(IGraphSnapshot snapshot)
        {
            snapshot.X = Widget.ViewportLocation.X;
            snapshot.Y = Widget.ViewportLocation.Y;
            snapshot.Zoom = Widget.ViewportZoom;
        }

        public void RestoreSnapshot(IGraphSnapshot snapshot)
        {
            Widget.ViewportLocation = new System.Windows.Point(snapshot.X, snapshot.Y);
            Widget.ViewportZoom = snapshot.Zoom;
        }
    }

    public class BPGraph : BPGraph<IGraphWidget>
    {
        public BPGraph(IGraphWidget editor, INodeFactory nodeFactory) : base(editor, nodeFactory)
        {
        }
    }
}
