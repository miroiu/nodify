using System.Windows;
using System.Windows.Input;

namespace Nodify.Playground
{
    public class ConnectionViewModel : ObservableObject
    {
        private NodifyEditorViewModel _graph = default!;
        public NodifyEditorViewModel Graph
        {
            get => _graph;
            internal set => SetProperty(ref _graph, value);
        }

        private ConnectorViewModel _input = default!;
        public ConnectorViewModel Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        private ConnectorViewModel _output = default!;
        public ConnectorViewModel Output
        {
            get => _output;
            set => SetProperty(ref _output, value);
        }

        public void Split(Point point)
            => Graph.Schema.SplitConnection(this, point);

        public void Remove()
            => Graph.Connections.Remove(this);

        public ICommand SplitCommand { get; }
        public ICommand DisconnectCommand { get; }

        public ConnectionViewModel()
        {
            SplitCommand = new DelegateCommand<Point>(Split);
            DisconnectCommand = new DelegateCommand(Remove);
        }
    }
}
