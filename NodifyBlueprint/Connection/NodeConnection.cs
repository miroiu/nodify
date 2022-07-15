using System.Windows;

namespace NodifyBlueprint
{
    public class NodeConnection : IConnection
    {
        public NodeConnection(IConnector source, IConnector target)
        {
            Source = source;
            Target = target;

            source.AddConnection(this);
            target.AddConnection(this);
        }

        public IConnector Source { get; }
        public IConnector Target { get; }

        public IGraph Graph => Source.Node.Graph;

        public virtual void Disconnect()
        {
            Graph.Disconnect(this);
        }

        public virtual void Split(Point location)
        {
            Graph.Split(this, location);
        }
    }
}