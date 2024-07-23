using System;
using System.Windows;

namespace Nodify
{
    /// <summary>
    /// Represents the method that will handle <see cref="Minimap.Zoom"/> routed event.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    public delegate void ZoomEventHandler(object sender, ZoomEventArgs e);

    /// <summary>
    /// Provides data for <see cref="Minimap.Zoom"/> routed event.
    /// </summary>
    public class ZoomEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomEventArgs"/> class using the specified <see cref="Zoom"/> and <see cref="Location"/>.
        /// </summary>
        public ZoomEventArgs(double zoom, Point location)
        {
            Zoom = zoom;
            Location = location;
        }

        /// <summary>
        /// Gets the zoom amount.
        /// </summary>
        public double Zoom { get; }

        /// <summary>
        /// Gets the location where the editor should zoom in.
        /// </summary>
        public Point Location { get; }
    }
}
