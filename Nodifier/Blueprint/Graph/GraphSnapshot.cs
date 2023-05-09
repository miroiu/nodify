using System.Collections.Generic;

namespace Nodifier.Blueprint
{
    public interface IGraphSnapshot
    {
        double X { get; set; }
        double Y { get; set; }
        double Zoom { get; set; }

        IReadOnlyCollection<GraphNodeSnapshot> Nodes { get; set; }
    }

    public class GraphNodeSnapshot
    {
        public INodeSnapshot Snapshot { get; set; }
        public string NodeType { get; set; }
        public string NodeId { get; set; }
    }

    public class GraphSnapshot : IGraphSnapshot
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Zoom { get; set; }
        public IReadOnlyCollection<GraphNodeSnapshot> Nodes { get; set; }
    }
}
