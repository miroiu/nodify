using System.Windows;

namespace Nodifier
{
    public abstract class GraphDecorator : PropertyChangedBase, IGraphDecorator
    {
        public IEditor Editor { get; }

        private Point _location;
        public Point Location
        {
            get => _location;
            set => SetAndNotify(ref _location, value);
        }

        private Size _size;
        public Size Size
        {
            get => _size;
            set => SetAndNotify(ref _size, value);
        }

        public GraphDecorator(IEditor editor)
        {
            Editor = editor;
        }
    }
}
