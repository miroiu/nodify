using Stylet;
using System.Collections.Generic;
using System.Windows;

namespace NodifyBlueprint
{
    public class BaseConnector : PropertyChangedBase, IConnector
    {
        public BaseConnector(IGraphElement node)
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
            if (connection.Source == this || connection.Target == this)
            {
                IsConnected = true;
                _connections.Add(connection);
            }
        }

        public virtual void RemoveConnection(IConnection connection)
        {
            if (connection.Source == this || connection.Target == this)
            {
                _connections.Remove(connection);
                IsConnected = _connections.Count > 0;
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

    public interface IRelayConnector : IConnector
    {
        IConnector Source { get; }
        IConnector Target { get; }
    }

    public class RelayConnector : BaseConnector, IRelayConnector
    {
        public IConnector Source { get; private set; } = default!;
        public IConnector Target { get; private set; } = default!;

        public override void AddConnection(IConnection connection)
        {
            Source = connection.Source != this ? connection.Source : connection.Target;
            Target = connection.Target != this ? connection.Target : connection.Source;

            base.AddConnection(connection);
        }

        public RelayConnector(IRelayNode node) : base(node)
        {
        }
    }
}
