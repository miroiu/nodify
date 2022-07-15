using Nodify;
using Nodifier.Views;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Nodifier
{
    public class Graph : IGraph, IViewAware
    {
        protected readonly BindableCollection<IGraphElement> _elements = new BindableCollection<IGraphElement>();
        public IReadOnlyCollection<IGraphElement> Elements => _elements;

        protected readonly BindableCollection<IGraphElement> _selectedElements = new BindableCollection<IGraphElement>();
        public IReadOnlyCollection<IGraphElement> SelectedElements => _selectedElements;

        protected readonly BindableCollection<IConnection> _connections = new BindableCollection<IConnection>();
        public IReadOnlyCollection<IConnection> Connections => _connections;

        public virtual IPendingConnection PendingConnection { get; }

        private NodifyEditor? _editor;
        UIElement IViewAware.View => _editor!;

        void IViewAware.AttachView(UIElement view)
        {
            if (view is INodifyEditorAware editorAware)
            {
                _editor = editorAware.Editor;
            }
            else
            {
                throw new InvalidOperationException($"The view of the {nameof(Graph)} should provide an editor instance by implementing {nameof(INodifyEditorAware)}.");
            }
        }

        public Graph()
        {
            PendingConnection = new PendingConnection(this);
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
            _editor?.BringIntoView(new Point(x, y));
        }

        public virtual bool TryConnect(IConnector source, IConnector target)
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
                IConnection connection = CreateConnection(source, target);
                _connections.Add(connection);
            }

            return canConnect;
        }

        protected virtual IConnection CreateConnection(IConnector source, IConnector target)
        {
            return new NodeConnection(source, target);
        }

        protected virtual bool CanConnect(IConnector source, IConnector target)
        {
            bool canConnect = source != target
                && source.Node != target.Node
                && source.Node.Graph == target.Node.Graph;

            return canConnect;
        }

        public virtual bool TryConnect(IConnector source, IGraphElement target)
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

        public virtual void Disconnect(IConnector connector)
        {
            var connections = _connections.Where(c => c.Source == connector || c.Target == connector).ToList();
            connections.ForEach(c =>
            {
                c.Source.RemoveConnection(c);
                c.Target.RemoveConnection(c);
            });
            _connections.RemoveRange(connections);
        }

        public virtual void Disconnect(IConnection connection)
        {
            RemoveConnection(connection);
        }

        public virtual void Split(IConnection connection, Point location)
        {
            var node = new RelayNode(this)
            {
                Location = location
            };

            var sourceCon = CreateConnection(connection.Source, node.Connector);
            var targetCon = CreateConnection(node.Connector, connection.Target);

            _connections.Add(sourceCon);
            _connections.Add(targetCon);

            AddElement(node);

            connection.Disconnect();
        }

        protected virtual void RemoveConnection(IConnection connection)
        {
            connection.Source.RemoveConnection(connection);
            connection.Target.RemoveConnection(connection);
            _connections.Remove(connection);
        }
    }
}
