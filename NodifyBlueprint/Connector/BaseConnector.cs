using Stylet;
using System.Collections.Generic;
using System.Windows;

namespace NodifyBlueprint
{
    public abstract class BaseConnector : PropertyChangedBase, IConnector
    {
        public BaseConnector(IGraphElement node)
        {
            Node = node;
        }

        public IGraphElement Node { get; }

        private string? _displayName;
        public string? Title
        {
            get => _displayName;
            set => SetAndNotify(ref _displayName, value);
        }

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
            set => SetAndNotify(ref _isConnected, value);
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
    }

    // Marker interface
    public interface IInputConnector : IConnector
    {
    }

    // Marker interface
    public interface IRelayConnector : IConnector
    {
        IConnector Source { get; }
        IConnector Target { get; }
    }

    // Marker interface
    public interface IOutputConnector : IConnector
    {
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

    public class InputConnector : BaseConnector, IInputConnector
    {
        public InputConnector(IGraphNode node) : base(node)
        {
        }
    }

    public class OutputConnector : BaseConnector, IOutputConnector
    {
        public OutputConnector(IGraphNode node) : base(node)
        {
        }
    }
}
