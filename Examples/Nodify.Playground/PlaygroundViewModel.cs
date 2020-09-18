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

        private void ResetGraph()
        {
            GraphViewModel.Nodes.Clear();
            GraphViewModel.Viewport.Offset = new System.Windows.Point(0, 0);
            GraphViewModel.Viewport.Scale = 1.0d;
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

        private async void GenerateRandomNodes()
        {
            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(100));
            GraphViewModel.Nodes.Clear();
            await CopyToAsync(nodes, GraphViewModel.Nodes);

            if (ShouldConnectNodes)
            {
                var connections = RandomNodesGenerator.GenerateConnections<GraphSchema>(GraphViewModel.Nodes);
                await CopyToAsync(connections, GraphViewModel.Connections);
            }
        }

        private async void ToggleConnections()
        {
            if (ShouldConnectNodes)
            {
                var connections = RandomNodesGenerator.GenerateConnections<GraphSchema>(GraphViewModel.Nodes);
                await CopyToAsync(connections, GraphViewModel.Connections);
            }
            else
            {
                GraphViewModel.Connections.Clear();
            }
        }

        private async void PerformanceTest()
        {
            int count = 1000;
            int distance = 500;
            int size = count / (int)Math.Sqrt(count);

            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(count)
            {
                NodeLocationGenerator = (s, i) => new System.Windows.Point(i % size * distance, i / size * distance)
            });
            GraphViewModel.Nodes.Clear();
            await CopyToAsync(nodes, GraphViewModel.Nodes);

            if (ShouldConnectNodes)
            {
                var connections = RandomNodesGenerator.GenerateConnections<GraphSchema>(GraphViewModel.Nodes);
                await CopyToAsync(connections, GraphViewModel.Connections);
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
