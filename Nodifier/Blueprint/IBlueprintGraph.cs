namespace Nodifier.Blueprint
{
    public interface IBlueprintGraph
    {
        IGraphWidget Widget { get; }

        IActionsHistory History { get; }

        TNode AddNode<TNode>()
            where TNode : IBlueprintNode;
    }

    public class BPGraph<TEditorWidget> : IBlueprintGraph
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
    }

    public class BPGraph : BPGraph<IGraphWidget>
    {
        public BPGraph(IGraphWidget editor, INodeFactory nodeFactory) : base(editor, nodeFactory)
        {
        }
    }
}
