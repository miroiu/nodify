﻿using System;
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

        public virtual IPendingConnection PendingConnection { get; }
        
        public GraphEditor(IActionsHistory history) : base(history)
        {
            PendingConnection = new PendingConnection(this);
        }

        public GraphEditor() : this(new ActionsHistory())
        {

        }

        public virtual void AddDecorator(IGraphDecorator decorator)
        {
            _decorators.Add(decorator);
        }

        public virtual void RemoveDecorator(IGraphDecorator decorator)
        {
            _decorators.Remove(decorator);
        }

        public virtual void AddElement(IGraphElement node)
        {
            _elements.Add(node);
            History.Record(() => AddElement(node), () => RemoveElement(node), nameof(AddElement));
        }

        public virtual void RemoveElement(IGraphElement node)
        {
            _elements.Remove(node);
            History.Record(() => RemoveElement(node), () => AddElement(node), nameof(RemoveElement));
        }

        public virtual void AddElements(IEnumerable<IGraphElement> nodes)
        {
            var newNodes = nodes.ToList();
            _elements.AddRange(newNodes);
            History.Record(() => AddElements(newNodes), () => RemoveElements(newNodes), nameof(AddElements));
        }

        public virtual void RemoveElements(IEnumerable<IGraphElement> nodes)
        {
            var newNodes = nodes.ToList();
            _elements.RemoveRange(nodes);
            History.Record(() => RemoveElements(newNodes), () => AddElements(newNodes), nameof(RemoveElements));
        }

        public void DeleteSelection()
            => RemoveElements(SelectedElements);

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
