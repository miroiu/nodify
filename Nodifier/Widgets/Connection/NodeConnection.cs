using System.Windows;

namespace Nodifier
{
    public interface IConnection : ICanDisconnect
    {
        IGraphWidget Graph { get; }

        IConnector Source { get; }
        IConnector Target { get; }

        void Split(Point location);
    }

    public class NodeConnection : IConnection
    {
        public NodeConnection(IConnector source, IConnector target)
        {
            if (source.Node.Graph != target.Node.Graph)
            {
                throw new GraphException($"The {nameof(source)} and {nameof(target)} must be in the same graph.");
            }

            Source = source;
            Target = target;
        }

        public IConnector Source { get; }
        public IConnector Target { get; }

        public IGraphWidget Graph => Source.Node.Graph;

        public virtual void Disconnect()
        {
            Graph.RemoveConnection(this);
        }

        public virtual void Split(Point location)
        {
            Graph.Split(this, location);
        }
    }
}