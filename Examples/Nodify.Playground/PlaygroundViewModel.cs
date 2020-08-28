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
            ClearNodesCommand = new DelegateCommand(() => GraphViewModel.Nodes.Clear());
        }

        public ICommand GenerateRandomNodesCommand { get; }
        public ICommand PerformanceTestCommand { get; }
        public ICommand ToggleConnectionsCommand { get; }
        public ICommand ClearNodesCommand { get; }

        private bool _shouldConnectNodes;
        public bool ShouldConnectNodes
        {
            get => _shouldConnectNodes;
            set => SetProperty(ref _shouldConnectNodes, value);
        }

        private void GenerateRandomNodes()
        {
            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(20));
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
            var nodes = RandomNodesGenerator.GenerateNodes<FlowNodeViewModel>(new NodesGeneratorSettings(1000));
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
