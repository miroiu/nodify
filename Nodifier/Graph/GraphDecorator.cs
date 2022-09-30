using Stylet;
using System.Windows;

namespace Nodifier
{
    public abstract class GraphDecorator : PropertyChangedBase, IGraphDecorator
    {
        public IGraph Graph { get; }

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

        public GraphDecorator(IGraph graph)
        {
            Graph = graph;
        }
    }
}
