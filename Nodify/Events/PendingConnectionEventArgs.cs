using System;
using System.Windows;

namespace Nodify
{
    /// <summary>
    /// Represents the method that will handle <see cref="PendingConnection"/> related routed events.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    public delegate void PendingConnectionEventHandler(object? sender, PendingConnectionEventArgs e);
    
    /// <summary>
    /// Provides data for <see cref="PendingConnection"/> related routed events.
    /// </summary>
    public class PendingConnectionEventArgs : RoutedEventArgs
    {
        private readonly MouseEventArgs? mouseEventArgs;

        /// <summary>
        /// Initializes a new instance of the <see cref="PendingConnectionEventArgs"/> class using the specified <see cref="SourceConnector"/>.
        /// </summary>
        /// <param name="sourceConnector">The <see cref="FrameworkElement.DataContext"/> of a related <see cref="Connector"/>.</param>
        public PendingConnectionEventArgs(object sourceConnector, MouseEventArgs? mouseEventArgs)
        {
            this.mouseEventArgs = mouseEventArgs;
            SourceConnector = sourceConnector;
        }
        
        /// <summary>
        /// Gets or sets the <see cref="Connector.Anchor"/> of the <see cref="Connector"/> that raised this event.
        /// </summary>
        public Point Anchor { get; set; }
        
        /// <summary>
        /// Gets the <see cref="FrameworkElement.DataContext"/> of the <see cref="Connector"/> that started this <see cref="PendingConnection"/>.
        /// </summary>
        public object SourceConnector { get; }
        
        /// <summary>
        /// Gets or sets the <see cref="FrameworkElement.DataContext"/> of the target <see cref="Connector"/> when the <see cref="PendingConnection"/> is completed.
        /// </summary>
        public object? TargetConnector { get; set; }

        /// <summary>
        /// Gets or sets the distance from the <see cref="SourceConnector"/> in the X axis.
        /// </summary>
        public double OffsetX { get; set; }

        /// <summary>
        /// Gets or sets the distance from the <see cref="SourceConnector"/> in the Y axis.
        /// </summary>
        public double OffsetY { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether this <see cref="PendingConnection"/> was cancelled.
        /// </summary>
        public bool Canceled { get; set; }
        
        public Point GetPosition(Visual? relativeTo)
        {
            return mouseEventArgs?.GetPosition(relativeTo) ?? default;
        }
    }
}
