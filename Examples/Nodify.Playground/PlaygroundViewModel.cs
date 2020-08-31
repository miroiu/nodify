using System;
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

        private void GenerateRandomNodes()
        {
            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(100));
            GraphViewModel.Nodes.Clear();
            GraphViewModel.Nodes.AddRange(nodes);

            if (ShouldConnectNodes)
            {
                var connections = RandomNodesGenerator.GenerateConnections<GraphSchema>(GraphViewModel.Nodes);
                GraphViewModel.Connections.AddRange(connections);
            }
        }

        private void ToggleConnections()
        {
            if (ShouldConnectNodes)
            {
                var connections = RandomNodesGenerator.GenerateConnections<GraphSchema>(GraphViewModel.Nodes);
                GraphViewModel.Connections.AddRange(connections);
            }
            else
            {
                GraphViewModel.Connections.Clear();
            }
        }

        private void PerformanceTest()
        {
            int count = 1000;
            int distance = 400;
            int size = count / (int)Math.Sqrt(count);

            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(count)
            {
                NodeLocationGenerator = (s, i) => new System.Windows.Point(i % size * distance, i / size * distance)
            });
            GraphViewModel.Nodes.Clear();
            GraphViewModel.Nodes.AddRange(nodes);

            if (ShouldConnectNodes)
            {
                var connections = RandomNodesGenerator.GenerateConnections<GraphSchema>(GraphViewModel.Nodes);
                GraphViewModel.Connections.AddRange(connections);
            }
        }
    }
}
