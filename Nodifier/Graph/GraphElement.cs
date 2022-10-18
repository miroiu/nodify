using System.Windows;

namespace Nodifier
{
    public interface IGraphElement
    {
        IGraphEditor Graph { get; }

        Point Location { get; set; }
        Size Size { get; }
        bool IsDraggable { get; set; }
        bool IsSelectable { get; set; }
        bool IsSelected { get; set; }
    }

    public abstract class GraphElement : Undoable, IGraphElement
    {
        public IGraphEditor Graph { get; }

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

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndNotify(ref _isSelected, value);
        }

        private bool _isSelectable = true;
        public bool IsSelectable
        {
            get => _isSelectable;
            set => SetAndNotify(ref _isSelectable, value);
        }

        private bool _isDraggable = true;
        public bool IsDraggable
        {
            get => _isDraggable;
            set => SetAndNotify(ref _isDraggable, value);
        }

        public GraphElement(IGraphEditor graph) : base(graph.History)
        {
            Graph = graph;

            ConfigurePoperty(nameof(Location), PropertyFlags.TrackHistory | PropertyFlags.Serialize);
            ConfigurePoperty(nameof(IsSelected), PropertyFlags.TrackHistory);
            ConfigurePoperty(nameof(IsDraggable), PropertyFlags.Serialize);
            ConfigurePoperty(nameof(IsSelectable), PropertyFlags.Serialize);
        }
    }
}
