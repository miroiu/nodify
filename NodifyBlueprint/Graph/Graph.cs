using Stylet;
using System.Collections.Generic;
using System.Linq;

namespace NodifyBlueprint
{
    public class Graph : IGraph
    {
        private readonly BindableCollection<IGraphElement> _elements = new BindableCollection<IGraphElement>();
        public IReadOnlyCollection<IGraphElement> Elements => _elements;

        private readonly BindableCollection<IGraphElement> _selectedElements = new BindableCollection<IGraphElement>();
        public IReadOnlyCollection<IGraphElement> SelectedElements => _selectedElements;

        private readonly BindableCollection<IConnection> _connections = new BindableCollection<IConnection>();
        public IReadOnlyCollection<IConnection> Connections => _connections;

        public virtual IPendingConnection PendingConnection { get; }

        public IGraphSchema Schema { get; }

        public Graph(IGraphSchema schema)
        {
            PendingConnection = new PendingConnection(this);
            Schema = schema;
        }

        public Graph() : this(GraphSchema.Default)
        {
        }

        public virtual void AddElement(IGraphElement node)
        {
            _elements.Add(node);
        }

        public virtual void RemoveElement(IGraphElement node)
        {
            _elements.Remove(node);
        }

        public void AddElements(IEnumerable<IGraphElement> nodes)
        {
            _elements.AddRange(nodes);
        }

        public void RemoveElements(IEnumerable<IGraphElement> nodes)
        {
            _elements.RemoveRange(nodes);
        }

        public virtual void FocusLocation(double x, double y)
        {
            // Need access to the editor control instance (could attach it in code behind or use the IViewAware interface)
        }

        public void TryConnect(IConnector source, IConnector target)
        {
            bool canConnect = Schema.CanConnect(source, target);

            if (canConnect)
            {
                var connection = CreateConnection(source, target);
                _connections.Add(connection);
            }
        }

        public virtual void TryConnect(IConnector source, IGraphElement target)
        {
        }

        protected virtual IConnection CreateConnection(IConnector source, IConnector target)
        {
            return new NodeConnection(source, target);
        }

        public virtual void Disconnect(IConnector connector)
        {
            connector.IsConnected = false;

            var connections = _connections.Where(c => c.Source == connector || c.Target == connector).ToList();
            connections.ForEach(c =>
            {
                c.Source.IsConnected = false;
                c.Target.IsConnected = false;
            });
            _connections.RemoveRange(connections);
        }

        public virtual void Disconnect(IConnection connection)
        {
            connection.Source.IsConnected = false;
            connection.Target.IsConnected = false;
            _connections.Remove(connection);
        }

        public void Disconnect(IGraphNode node)
        {
            var inputConnections = node.Input.SelectMany(c => c.Connections);
            var outputConnections = node.Output.SelectMany(c => c.Connections);

            _connections.RemoveRange(inputConnections);
            _connections.RemoveRange(outputConnections);
        }
    }
}
