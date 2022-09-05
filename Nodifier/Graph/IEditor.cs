using System;
using System.Windows;

namespace Nodifier
{
    public interface IEditor
    {
        Point ViewportLocation { get; set; }
        Size ViewportSize { get; }
        double ViewportZoom { get; set; }

        IEditorSettings Settings { get; }

        void FocusLocation(Point location);
        void ZoomIn();
        void ZoomOut();

        void SelectAll();
        void SelectArea(Rect area);
        void UnselectAll();
        void UnselectArea(Rect area);

        /// <summary>
        /// Called when the view is loaded. Here you can access the size of the elements.
        /// </summary>
        event EventHandler Initialized;
    }
}