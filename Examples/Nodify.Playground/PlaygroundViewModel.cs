using System;
using System.Collections;
using System.Threading.Tasks;
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

        private int _connectionsBatchSize = 50;
        private int _nodesBatchSize = 10;

        private async void GenerateRandomNodes()
        {
            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(100));
            GraphViewModel.Nodes.Clear();
            await CopyToAsync(nodes, GraphViewModel.Nodes, _nodesBatchSize);

            if (ShouldConnectNodes)
            {
                var connections = RandomNodesGenerator.GenerateConnections<GraphSchema>(GraphViewModel.Nodes);
                await CopyToAsync(connections, GraphViewModel.Connections, _connectionsBatchSize);
            }
        }

        private async void ToggleConnections()
        {
            if (ShouldConnectNodes)
            {
                var connections = RandomNodesGenerator.GenerateConnections<GraphSchema>(GraphViewModel.Nodes);
                await CopyToAsync(connections, GraphViewModel.Connections, _connectionsBatchSize);
            }
            else
            {
                GraphViewModel.Connections.Clear();
            }
        }

        private async void PerformanceTest()
        {
            int count = 1000;
            int distance = 400;
            int size = count / (int)Math.Sqrt(count);

            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(count)
            {
                NodeLocationGenerator = (s, i) => new System.Windows.Point(i % size * distance, i / size * distance)
            });
            GraphViewModel.Nodes.Clear();
            await CopyToAsync(nodes, GraphViewModel.Nodes, _nodesBatchSize);

            if (ShouldConnectNodes)
            {
                var connections = RandomNodesGenerator.GenerateConnections<GraphSchema>(GraphViewModel.Nodes);
                await CopyToAsync(connections, GraphViewModel.Connections, _connectionsBatchSize);
            }
        }

        private async Task CopyToAsync(IList source, IList target, int batches = 5)
        {
            if (Async)
            {
                for (int i = 0; i <= source.Count - batches;)
                {
                    for (int j = 0; j < batches; j++, i++)
                    {
                        target.Add(source[i]);
                    }

                    await Task.Delay(1);
                }
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
