using System.Windows;

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

        private Size _size;
        public Size Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        private bool _isResizable = true;
        public bool IsResizable
        {
            get => _isResizable;
            set => SetProperty(ref _isResizable, value);
        }
    }
}
