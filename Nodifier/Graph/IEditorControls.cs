using System.Windows;

namespace Nodifier
{
    public interface IEditorControls
    {
        void FocusLocation(Point location);
        void ZoomIn();
        void ZoomOut();
        
        void SelectAll();
        void SelectArea(Rect area);
        void UnselectAll();
        void UnselectArea(Rect area);
    }
}