using System.Collections.Generic;
using System.Windows;

namespace Nodifier
{
    public interface IConnector : ICanDisconnect
    {
        IGraphElement Node { get; }
        Point Anchor { get; }
        bool IsConnected { get; }
        IReadOnlyCollection<IConnection> Connections { get; }

        void AddConnection(IConnection connection);
        void RemoveConnection(IConnection connection);

        bool TryConnectTo(IGraphElement element);
        bool TryConnectTo(IConnector other);
    }

    public class BaseConnector : Undoable, IConnector
    {
        public BaseConnector(IGraphElement node) : base(node.Graph.History)
        {
            Node = node;
        }

        public IGraphElement Node { get; }

        private Point _anchor;
        public Point Anchor
        {
            get => _anchor;
            set => SetAndNotify(ref _anchor, value);
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            protected set => SetAndNotify(ref _isConnected, value);
        }

        private readonly HashSet<IConnection> _connections = new HashSet<IConnection>();
        public IReadOnlyCollection<IConnection> Connections => _connections;

        public void Disconnect() => Node.Graph.Disconnect(this);

        public virtual void AddConnection(IConnection connection)
        {
            if (!(connection.Source == this || connection.Target == this))
            {
                throw new GraphException($"The connector must be a {nameof(connection.Source)} or a {nameof(connection.Target)} of the connection.");
            }

            if (_connections.Add(connection))
            {
                IsConnected = true;
                Node.Graph.AddConnection(connection);
            }
        }

        public virtual void RemoveConnection(IConnection connection)
        {
            if (!(connection.Source == this || connection.Target == this))
            {
                throw new GraphException($"The connector must be a {nameof(connection.Source)} or a {nameof(connection.Target)} of the connection.");
            }

            if (_connections.Remove(connection))
            {
                IsConnected = _connections.Count > 0;
                Node.Graph.RemoveConnection(connection);
            }
        }

        public virtual bool TryConnectTo(IGraphElement element)
        {
            return Node.Graph.TryConnect(this, element);
        }

        public virtual bool TryConnectTo(IConnector other)
        {
            return Node.Graph.TryConnect(this, other);
        }
    }
}
