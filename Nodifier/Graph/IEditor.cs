using System;
using System.Windows;

namespace Nodifier
{
    public interface IEditor
    {
        Point ViewportLocation { get; set; }
        Size ViewportSize { get; }
        double ViewportZoom { get; set; }
        double MinViewportZoom { get; set; }
        double MaxViewportZoom { get; set; }

        double GridSnapSize { get; set; }

        bool DisableZooming { get; set; }
        bool DisablePanning { get; set; }
        bool DisableAutoPanning { get; set; }
        bool EnableRealtimeSelection { get; set; }

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