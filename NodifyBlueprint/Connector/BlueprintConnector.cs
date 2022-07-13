using Stylet;
using System.Collections.Generic;
using System.Windows;

namespace NodifyBlueprint
{
    public abstract class BlueprintConnector : PropertyChangedBase, IConnector
    {
        public BlueprintConnector(IGraphNode node)
        {
            Node = node;
        }

        public IGraphNode Node { get; }

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

        private List<IConnection> _connections = new List<IConnection>();
        public IReadOnlyCollection<IConnection> Connections => _connections;
        public void Disconnect() => Node.Graph.Disconnect(this);

        public void AddConnection(IConnection connection)
        {
            if (connection.Source == this || connection.Target == this)
            {
                _connections.Add(connection);
            }
        }
    }

    // Marker interface
    public interface IInputConnector : IConnector
    {
    }

    // Marker interface
    public interface IOutputConnector : IConnector
    {
    }

    public class InputConnector : BlueprintConnector, IInputConnector
    {
        public InputConnector(IGraphNode node) : base(node)
        {
        }
    }

    public class OutputConnector : BlueprintConnector, IOutputConnector
    {
        public OutputConnector(IGraphNode node) : base(node)
        {
        }
    }
}
