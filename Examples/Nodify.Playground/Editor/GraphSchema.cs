using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Nodify.Playground
{
    public class GraphSchema
    {
        #region Add Connection

        public bool CanAddConnection(ConnectorViewModel source, object target)
        {
            if (target is ConnectorViewModel con)
            {
                return source != con
                    && source.Node != con.Node
                    && source.Node.Graph == con.Node.Graph
                    && source.AllowsNewConnections()
                    && con.AllowsNewConnections()
                    && source.Flow != con.Flow
                    && !source.IsConnectedTo(con);
                    // TODO:
                    //&& (source.Flow != con.Flow || source.Node is KnotNodeViewModel || con.Node is KnotNodeViewModel);
            }
            else if (source.AllowsNewConnections() && target is FlowNodeViewModel node)
            {
                var allConnectors = source.Flow == ConnectorFlow.Input ? node.Output : node.Input;
                return allConnectors.Any(c => c.AllowsNewConnections());
            }

            return false;
        }

        public bool TryAddConnection(ConnectorViewModel source, object? target)
        {
            if (target != null && CanAddConnection(source, target))
            {
                if (target is ConnectorViewModel connector)
                {
                    AddConnection(source, connector);
                    return true;
                }
                else if (target is FlowNodeViewModel node)
                {
                    AddConnection(source, node);
                    return true;
                }
            }

            return false;
        }

        private void AddConnection(ConnectorViewModel source, ConnectorViewModel target)
        {
            var sourceIsInput = source.Flow == ConnectorFlow.Input;

            source.Node.Graph.Connections.Add(new ConnectionViewModel
            {
                Input = sourceIsInput ? source : target,
                Output = sourceIsInput ? target : source
            });
        }

        private void AddConnection(ConnectorViewModel source, FlowNodeViewModel target)
        {
            var allConnectors = source.Flow == ConnectorFlow.Input ? target.Output : target.Input;
            var connector = allConnectors.First(c => c.AllowsNewConnections());

            AddConnection(source, connector);
        }

        #endregion

        public void DisconnectConnector(ConnectorViewModel connector)
        {
            var graph = connector.Node.Graph;
            var connections = connector.Connections.ToList();
            connections.ForEach(c => graph.Connections.Remove(c));
        }

        public void SplitConnection(ConnectionViewModel connection, Point location)
        {
            var connector = connection.Output;

            var knot = new KnotNodeViewModel
            {
                Location = location,
                Connector = new ConnectorViewModel
                {
                    MaxConnections = connector == connection.Output ? connection.Input.MaxConnections : connection.Output.MaxConnections
                }
            };
            connection.Graph.Nodes.Add(knot);

            AddConnection(connector, knot.Connector);
            AddConnection(knot.Connector, connection.Input);

            connection.Remove();
        }

        public void AddCommentAroundNodes(IList<NodeViewModel> nodes, string? text = default)
        {
            var rect = nodes.GetBoundingBox(50);
            var comment = new CommentNodeViewModel
            {
                Location = rect.Location,
                Size = rect.Size,
                Title = "New comment"
            };

            nodes[0].Graph.Nodes.Add(comment);
        }
    }
}
