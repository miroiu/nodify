using System.Windows;

namespace Nodifier
{
    public interface IGraphDecorator
    {
        IGraphEditor Editor { get; }
        Point Location { get; set; }
        Size Size { get; }
    }

    public abstract class GraphDecorator : PropertyChangedBase, IGraphDecorator
    {
        public IGraphEditor Editor { get; }

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

        public GraphDecorator(IGraphEditor editor)
        {
            Editor = editor;
        }
    }
}
