using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Nodifier
{
    public partial class GraphEditor : Undoable, IGraphEditor
    {
        protected readonly BindableCollection<IGraphElement> _elements = new BindableCollection<IGraphElement>();
        public IReadOnlyCollection<IGraphElement> Elements => _elements;

        protected readonly BindableCollection<IGraphElement> _selectedElements = new BindableCollection<IGraphElement>();
        public IReadOnlyCollection<IGraphElement> SelectedElements => _selectedElements;

        protected readonly BindableCollection<IConnection> _connections = new BindableCollection<IConnection>();
        public IReadOnlyCollection<IConnection> Connections => _connections;

        protected readonly BindableCollection<IGraphDecorator> _decorators = new BindableCollection<IGraphDecorator>();
        public IReadOnlyCollection<IGraphDecorator> Decorators => _decorators;

        public IPendingConnection PendingConnection { get; }

        public GraphEditor(IActionsHistory history) : base(history)
        {
            PendingConnection = CreatePendingConnection();
        }

        public GraphEditor() : this(new ActionsHistory())
        {

        }

        public void AddDecorator(IGraphDecorator decorator)
        {
            _decorators.Add(decorator);
            History.Record(() => AddDecorator(decorator), () => RemoveDecorator(decorator), nameof(AddDecorator));
        }

        public void RemoveDecorator(IGraphDecorator decorator)
        {
            _decorators.Remove(decorator);
            History.Record(() => RemoveDecorator(decorator), () => AddDecorator(decorator), nameof(RemoveDecorator));
        }

        public void AddElement(IGraphElement node)
        {
            _elements.Add(node);
            History.Record(() => AddElement(node), () => RemoveElement(node), nameof(AddElement));
        }

        public void RemoveElement(IGraphElement node)
        {
            using (History.Batch(nameof(RemoveElement)))
            {
                History.Record(() => RemoveElement(node), () => AddElement(node), nameof(RemoveElement));
                _elements.Remove(node);

                if (node is ICanDisconnect canDisconnect)
                {
                    canDisconnect.Disconnect();
                }
            }
        }

        public void AddElements(IEnumerable<IGraphElement> nodes)
        {
            var newNodes = nodes.ToList();
            _elements.AddRange(newNodes);
            History.Record(() => AddElements(newNodes), () => RemoveElements(newNodes), nameof(AddElements));
        }

        public void RemoveElements(IEnumerable<IGraphElement> nodes)
        {
            using (History.Batch(nameof(RemoveElements)))
            {
                var newNodes = nodes.ToList();
                _elements.RemoveRange(nodes);
                History.Record(() => RemoveElements(newNodes), () => AddElements(newNodes), nameof(RemoveElements));

                foreach (var graphNode in newNodes.Where(x => x is ICanDisconnect).Cast<ICanDisconnect>())
                {
                    graphNode.Disconnect();
                }
            }
        }

        public void DeleteSelection()
            => RemoveElements(SelectedElements);

        public void Disconnect(IConnector connector)
        {
            var connections = _connections.Where(c => c.Source == connector || c.Target == connector).ToList();
            using (History.Batch(nameof(Disconnect)))
            {
                connections.ForEach(c => RemoveConnection(c));
            }
            _connections.RemoveRange(connections);
        }

        public void Split(IConnection connection, Point location)
        {
            var node = new RelayNode(this)
            {
                Location = location
            };

            using (History.Batch(nameof(Split)))
            {
                NewConnection(connection.Source, node.Connector);
                NewConnection(node.Connector, connection.Target);

                AddElement(node);

                connection.Disconnect();
            }
        }

        public void AddConnection(IConnection connection)
        {
            if (connection.Graph != this)
            {
                throw new GraphException("The connection must be in this graph.");
            }

            if (!_connections.Contains(connection))
            {
                _connections.Add(connection);

                using (History.Batch(nameof(AddConnection)))
                {
                    connection.Source.AddConnection(connection);
                    connection.Target.AddConnection(connection);
                    History.Record(() => AddConnection(connection), () => RemoveConnection(connection), nameof(AddConnection));
                }
            }
        }

        public void RemoveConnection(IConnection connection)
        {
            if (connection.Graph != this)
            {
                throw new GraphException("The connection must be in this graph.");
            }

            if (_connections.Contains(connection))
            {
                using (History.Batch(nameof(RemoveConnection)))
                {
                    connection.Source.RemoveConnection(connection);
                    connection.Target.RemoveConnection(connection);

                    _connections.Remove(connection);
                    History.Record(() => RemoveConnection(connection), () => AddConnection(connection), nameof(RemoveConnection));
                }
            }
        }

        public bool TryConnect(IConnector source, IConnector target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            bool canConnect = CanConnect(source, target);
            if (canConnect)
            {
                NewConnection(source, target);
            }

            return canConnect;
        }

        public bool TryConnect(IConnector source, IGraphElement target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target is IGraphNode node)
            {
                IConnector? connector = node.Input.FirstOrDefault(x => CanConnect(source, x)) ?? node.Output.FirstOrDefault(x => CanConnect(source, x));
                if (connector != null)
                {
                    return TryConnect(source, connector);
                }
            }
            else if (target is IRelayNode relay)
            {
                return TryConnect(source, relay.Connector);
            }

            return false;
        }

        private void NewConnection(IConnector source, IConnector target)
        {
            using (History.Batch(nameof(AddConnection)))
            {
                var conn = CreateConnection(source, target);
                AddConnection(conn);
            }
        }

        protected virtual IConnection CreateConnection(IConnector source, IConnector target)
            => new NodeConnection(source, target);

        protected virtual IPendingConnection CreatePendingConnection()
            => new PendingConnection(this);

        protected virtual bool CanConnect(IConnector source, IConnector target)
        {
            bool canConnect = source != target
                && source.Node != target.Node
                && source.Node.Graph == target.Node.Graph;

            return canConnect;
        }
    }
}
