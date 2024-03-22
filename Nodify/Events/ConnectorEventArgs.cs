using System;
using System.Windows;

namespace Nodify
{
    /// <summary>
    /// Represents the method that will handle <see cref="Connector"/> related routed events.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    public delegate void ConnectorEventHandler(object? sender, ConnectorEventArgs e);

    /// <summary>
    /// Provides data for <see cref="Nodify.Connector"/> related routed events.
    /// </summary>
    public class ConnectorEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorEventArgs"/> class using the specified <see cref="Connector"/>.
        /// </summary>
        /// <param name="connector">The <see cref="FrameworkElement.DataContext"/> of a related <see cref="Nodify.Connector"/>.</param>
        public ConnectorEventArgs(object connector)
            => Connector = connector;

        /// <summary>
        /// Gets or sets the <see cref="Nodify.Connector.Anchor"/> of the <see cref="Nodify.Connector"/> associated with this event.
        /// </summary>
        public Point Anchor { get; set; }

        /// <summary>
        /// Gets the <see cref="FrameworkElement.DataContext"/> of the <see cref="Nodify.Connector"/> associated with this event.
        /// </summary>
        public object Connector { get; }
    }
}
