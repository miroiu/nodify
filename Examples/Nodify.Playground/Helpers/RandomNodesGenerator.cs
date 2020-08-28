using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Nodify.Playground
{
    public struct NodesGeneratorSettings
    {
        public NodesGeneratorSettings(int count)
        {
            GridSnap = 15;
            MinNodesCount = MaxNodesCount = count;
            MinInputCount = MinOutputCount = 0;
            MaxInputCount = MaxOutputCount = 7;
        }

        public int GridSnap;
        public int MinNodesCount;
        public int MaxNodesCount;
        public int MinInputCount;
        public int MaxInputCount;
        public int MinOutputCount;
        public int MaxOutputCount;

        public int Snap(int x)
            => x / GridSnap * GridSnap;
    }

    public static class RandomNodesGenerator
    {
        private static readonly System.Random _rand = new System.Random();

        public static IList<T> GenerateNodes<T>(NodesGeneratorSettings settings)
            where T : FlowNodeViewModel, new()
        {
            var nodes = new List<T>();
            var count = _rand.Next(settings.MinNodesCount, settings.MaxNodesCount);

            for (int i = 0; i < count; i++)
            {
                var x = ((count / 2 - i + 1) * 100 + _rand.Next() % (15 * count)) % _rand.Next(100, 100 + count * 10);
                var y = ((count / 2 - i + 1) * 100 + _rand.Next() % (15 * count)) % _rand.Next(100, 100 + count * 10);

                x = settings.Snap(x);
                y = settings.Snap(y);

                var node = new T
                {
                    Title = $"Title {i}",
                    IsCompact = i % 2 == 0,
                    Location = new Point(x, y)
                };

                nodes.Add(node);
                node.Input.AddRange(GenerateConnectors(_rand.Next(settings.MinInputCount, settings.MaxInputCount)));
                node.Output.AddRange(GenerateConnectors(_rand.Next(settings.MinOutputCount, settings.MaxOutputCount)));
            }

            return nodes;
        }

        public static IList<ConnectionViewModel> GenerateConnections<TSchema>(IList<NodeViewModel> nodes)
            where TSchema : GraphSchema, new()
        {
            HashSet<NodeViewModel> visited = new HashSet<NodeViewModel>(nodes.Count);
            List<ConnectionViewModel> connections = new List<ConnectionViewModel>(nodes.Count);
            var schema = new TSchema();

            for (int i = 0; i < nodes.Count; i++)
            {
                var n1 = nodes[_rand.Next(0, nodes.Count)];
                var n2 = nodes[_rand.Next(0, nodes.Count)];

                if (n1 == n2 && !(visited.Add(n1) && visited.Add(n2)))
                {
                    continue;
                }

                List<ConnectorViewModel> input = n1 is FlowNodeViewModel flow ? flow.Input.ToList() :
                                                 n1 is KnotNodeViewModel knot ? new List<ConnectorViewModel> { knot.Connector } : new List<ConnectorViewModel>();

                List<ConnectorViewModel> output = n2 is FlowNodeViewModel flow2 ? flow2.Output.ToList() :
                                                  n2 is KnotNodeViewModel knot2 ? new List<ConnectorViewModel> { knot2.Connector } : new List<ConnectorViewModel>();

                connections.AddRange(ConnectPins(schema, input, output));
            }

            return connections;
        }

        public static IList<ConnectionViewModel> ConnectPins(GraphSchema schema, IList<ConnectorViewModel> source, IList<ConnectorViewModel> target)
        {
            List<ConnectionViewModel> con = new List<ConnectionViewModel>();

            for (int di = 0; di < target.Count; di++)
            {
                var outP = target[di];

                var conNum = _rand.Next(0, source.Count);
                for (int ci = 0; ci < conNum; ci++)
                {
                    var inP = source[_rand.Next(1, conNum)];

                    if (schema.CanAddConnection(inP, outP))
                    {
                        var isInput = inP.Flow == ConnectorFlow.Input;

                        con.Add(new ConnectionViewModel
                        {
                            Output = isInput ? inP : outP,
                            Input = isInput ? outP : inP
                        });
                    }
                }
            }

            return con;
        }

        public static IList<ConnectorViewModel> GenerateConnectors(int count, ConnectorType? type = null)
        {
            var list = new List<ConnectorViewModel>(count);

            for (int i = 1; i <= count; i++)
            {
                var next = _rand.Next(count);
                var newType = type ?? (next % 3 == 0 ? ConnectorType.Flow : ConnectorType.Data);

                var connector = new ConnectorViewModel
                {
                    Title = next % 2 == 0 ? $"Pin {i}" : null,
                    Type = newType
                };

                if (newType == ConnectorType.Flow)
                {
                    list.Insert(0, connector);
                }
                else
                {
                    list.Add(connector);
                }
            }

            return list;
        }
    }
}
