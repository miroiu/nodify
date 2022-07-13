using System.Windows;

namespace NodifyBlueprint
{
    public interface IGraphElement
    {
        IGraph Graph { get; }
        Point Location { get; set; }
        Size Size { get; }
        bool IsDraggable { get; set; }
        bool IsSelectable { get; set; }
        bool IsSelected { get; set; }
    }
}
