using System;
using System.Collections.Generic;
using System.Linq;

namespace Nodifier.Blueprint
{
    public class BPGraph<TEditorWidget, TSnapshot> : IBlueprintGraph, IMemento<TSnapshot>, IGraphMemento
        where TEditorWidget : class, IGraphWidget
        where TSnapshot : IGraphSnapshot, new()
    {
        private readonly INodeFactory _nodeFactory;

        IGraphWidget IBlueprintGraph.Widget => Widget;

        public TEditorWidget Widget { get; }

        public IActionsHistory History => Widget.History;

        private readonly List<IBlueprintNode> _nodes = new List<IBlueprintNode>(8);
        public IReadOnlyCollection<IBlueprintNode> Nodes => _nodes;

        public BPGraph(TEditorWidget editor, INodeFactory nodeFactory)
        {
            Widget = editor;
            _nodeFactory = nodeFactory;
        }

        public TNode AddNode<TNode>()
            where TNode : IBlueprintNode
        {
            return (TNode)AddNode(typeof(TNode));
        }

        public TSnapshot CreateSnapshot()
        {
            var snapshot = new TSnapshot
            {
                X = Widget.ViewportLocation.X,
                Y = Widget.ViewportLocation.Y,
                Zoom = Widget.ViewportZoom,

                Nodes = Nodes.Select(x => new GraphNodeSnapshot
                {
                    NodeType = x.GetType().AssemblyQualifiedName!,
                    Snapshot = ((INodeMemento)x).CreateSnapshot()
                }).ToList()
            };

            OnSnapshotCreating(snapshot);
            return snapshot;
        }

        IGraphSnapshot IGraphMemento.CreateSnapshot()
        {
            return CreateSnapshot();
        }

        void IGraphMemento.RestoreSnapshot(IGraphSnapshot snapshot)
        {
            RestoreSnapshot((TSnapshot)snapshot);
        }

        protected virtual void OnSnapshotCreating(TSnapshot snapshot)
        {
        }

        public void RestoreSnapshot(TSnapshot snapshot)
        {
            using (History.Batch(nameof(RestoreSnapshot)))
            {
                Widget.ViewportLocation = new System.Windows.Point(snapshot.X, snapshot.Y);
                Widget.ViewportZoom = snapshot.Zoom;

                _nodes.Clear();
                foreach (var nodeSnapshot in snapshot.Nodes)
                {
                    var node = AddNode(nodeSnapshot);
                }
            }
        }

        public IBlueprintNode AddNode(Type nodeType)
        {
            var node = _nodeFactory.Get(nodeType, this);
            _nodes.Add(node);
            Widget.AddElement(node.Widget);
            return node;
        }

        public IBlueprintNode AddNode(GraphNodeSnapshot snapshot)
        { 
            var type = Type.GetType(snapshot.NodeType);
            var node = AddNode(type);
            ((INodeMemento)node).RestoreSnapshot(snapshot.Snapshot);
            return node;
        }
    }

    public class BPGraph<TSnapshot> : BPGraph<IGraphWidget, TSnapshot>
        where TSnapshot : IGraphSnapshot, new()
    {
        public BPGraph(IGraphWidget editor, INodeFactory nodeFactory) : base(editor, nodeFactory)
        {
        }
    }

    public class BPGraph : BPGraph<IGraphWidget, GraphSnapshot>
    {
        public BPGraph(IGraphWidget editor, INodeFactory nodeFactory) : base(editor, nodeFactory)
        {
        }
    }
}
