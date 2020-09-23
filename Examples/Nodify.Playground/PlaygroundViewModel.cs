using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Nodify.Playground
{
    public class PlaygroundViewModel : ObservableObject
    {
        public NodifyEditorViewModel GraphViewModel { get; } = new NodifyEditorViewModel();

        public PlaygroundViewModel()
        {
            GenerateRandomNodesCommand = new DelegateCommand(GenerateRandomNodes);
            PerformanceTestCommand = new DelegateCommand(PerformanceTest);
            ToggleConnectionsCommand = new DelegateCommand(ToggleConnections);
            ResetCommand = new DelegateCommand(ResetGraph);

            BindingOperations.EnableCollectionSynchronization(GraphViewModel.Nodes, GraphViewModel.Nodes);
            BindingOperations.EnableCollectionSynchronization(GraphViewModel.Connections, GraphViewModel.Connections);
        }

        public ICommand GenerateRandomNodesCommand { get; }
        public ICommand PerformanceTestCommand { get; }
        public ICommand ToggleConnectionsCommand { get; }
        public ICommand ResetCommand { get; }

        private bool _shouldConnectNodes = true;
        public bool ShouldConnectNodes
        {
            get => _shouldConnectNodes;
            set => SetProperty(ref _shouldConnectNodes, value);
        }

        private bool _async = true;
        public bool Async
        {
            get => _async;
            set => SetProperty(ref _async, value);
        }

        private uint _minNodes = 10;
        public uint MinNodes
        {
            get => _minNodes;
            set => SetProperty(ref _minNodes, value);
        }

        private uint _maxNodes = 100;
        public uint MaxNodes
        {
            get => _maxNodes;
            set => SetProperty(ref _maxNodes, value);
        }

        private uint _minConnectors = 0;
        public uint MinConnectors
        {
            get => _minConnectors;
            set => SetProperty(ref _minConnectors, value);
        }

        private uint _maxConnectors = 7;
        public uint MaxConnectors
        {
            get => _maxConnectors;
            set => SetProperty(ref _maxConnectors, value);
        }

        private uint _performanceTestNodes = 1000;
        public uint PerformanceTestNodes
        {
            get => _performanceTestNodes;
            set => SetProperty(ref _performanceTestNodes, value);
        }

        private void ResetGraph()
        {
            GraphViewModel.Nodes.Clear();
            GraphViewModel.Viewport.Offset = new System.Windows.Point(0, 0);
            GraphViewModel.Viewport.Scale = 1.0d;
        }

        private async void GenerateRandomNodes()
        {
            if (MinNodes > MaxNodes)
            {
                MaxNodes = MinNodes;
            }

            if (MinConnectors > MaxConnectors)
            {
                MaxConnectors = MinConnectors;
            }

            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(MinNodes)
            {
                MinNodesCount = MinNodes,
                MaxNodesCount = MaxNodes,
                MinInputCount = MinConnectors,
                MaxInputCount = MaxConnectors,
                MinOutputCount = MinConnectors,
                MaxOutputCount = MaxConnectors,
                GridSnap = EditorSettings.Instance.GridSpacing
            });

            GraphViewModel.Nodes.Clear();
            await CopyToAsync(nodes, GraphViewModel.Nodes);

            if (ShouldConnectNodes)
            {
                await ConnectNodes();
            }
        }

        private async void ToggleConnections()
        {
            if (ShouldConnectNodes)
            {
                await ConnectNodes();
            }
            else
            {
                GraphViewModel.Connections.Clear();
            }
        }

        private async void PerformanceTest()
        {
            uint count = PerformanceTestNodes;
            int distance = 500;
            int size = (int)count / (int)Math.Sqrt(count);

            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(count)
            {
                NodeLocationGenerator = (s, i) => new System.Windows.Point(i % size * distance, i / size * distance),
                MinInputCount = MinConnectors,
                MaxInputCount = MaxConnectors,
                MinOutputCount = MinConnectors,
                MaxOutputCount = MaxConnectors,
                GridSnap = EditorSettings.Instance.GridSpacing
            });

            GraphViewModel.Nodes.Clear();
            await CopyToAsync(nodes, GraphViewModel.Nodes);

            if (ShouldConnectNodes)
            {
                await ConnectNodes();
            }
        }

        private async Task ConnectNodes()
        {
            var schema = new GraphSchema();
            var connections = RandomNodesGenerator.GenerateConnections(GraphViewModel.Nodes);

            if (Async)
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < connections.Count; i++)
                    {
                        var con = connections[i];
                        schema.TryAddConnection(con.Input, con.Output);
                    }
                });
            }
            else
            {
                for (int i = 0; i < connections.Count; i++)
                {
                    var con = connections[i];
                    schema.TryAddConnection(con.Input, con.Output);
                }
            }
        }

        private async Task CopyToAsync(IList source, IList target)
        {
            if (Async)
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < source.Count; i++)
                    {
                        target.Add(source[i]);
                    }
                });
            }
            else
            {
                for (int i = 0; i < source.Count; i++)
                {
                    target.Add(source[i]);
                }
            }
        }
    }
}
