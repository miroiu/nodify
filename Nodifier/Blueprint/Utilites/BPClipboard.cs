using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Nodifier.Blueprint
{
    public static class BPClipboard
    {
        private class BPSelectionSnapshot
        {
            public BPSelectionSnapshot(IEnumerable<GraphNodeSnapshot> nodes)
            {
                Nodes = nodes.ToList();
                Location = GetTopLeft(Nodes);
            }

            private static Point GetTopLeft(IEnumerable<GraphNodeSnapshot> nodes)
            {
                double minX = double.MaxValue;
                double minY = double.MaxValue;

                foreach (var node in nodes)
                {
                    if (node.Snapshot.X < minX)
                    {
                        minX = node.Snapshot.X;
                    }

                    if (node.Snapshot.Y < minY)
                    {
                        minY = node.Snapshot.Y;
                    }
                }

                return new Point(minX, minY);
            }

            public IReadOnlyCollection<GraphNodeSnapshot> Nodes { get; }
            public Point Location { get; }
        }

        private static BPSelectionSnapshot? _selection;

        public static void CopySelection(IBlueprintGraph graph)
        {
            var nodes = ((IGraphMemento)graph).CreateSnapshot().Nodes.Where(x => x.Snapshot.IsSelected);
            var selection = new BPSelectionSnapshot(nodes);
            _selection = selection;
        }

        public static void PasteSelection(IBlueprintGraph graph)
        {
            if (_selection == null || _selection.Nodes.Count == 0) return;

            using (graph.History.Batch(nameof(PasteSelection)))
            {
                graph.Widget.UnselectAll();
                foreach (var nodeSnapshot in _selection.Nodes)
                {
                    var locationOffset = graph.Widget.MouseLocation - _selection.Location;

                    var node = graph.AddNode(nodeSnapshot);
                    ((INodeMemento)node).RestoreSnapshot(nodeSnapshot.Snapshot);
                    node.Widget.Location += (Vector)locationOffset;
                }
            }
        }
    }
}
