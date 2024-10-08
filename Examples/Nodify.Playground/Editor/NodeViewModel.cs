using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nodify.Playground
{
    public abstract class NodeViewModel : ObservableObject
    {
        private NodifyEditorViewModel _graph = default!;
        public NodifyEditorViewModel Graph
        {
            get => _graph;
            internal set => SetProperty(ref _graph, value);
        }

        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public Orientation Orientation { get; protected set; }

        public ICommand DeleteCommand { get; }

        public NodeViewModel()
        {
            DeleteCommand = new DelegateCommand(() => Graph.Nodes.Remove(this));
        }
    }
}
