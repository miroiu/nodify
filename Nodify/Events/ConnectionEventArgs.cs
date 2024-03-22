using System;
using System.Windows;

namespace Nodify
{
    /// <summary>
    /// Represents the method that will handle <see cref="BaseConnection"/> related routed events.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    public delegate void ConnectionEventHandler(object? sender, ConnectionEventArgs e);

    /// <summary>
    /// Provides data for <see cref="BaseConnection"/> related routed events.
    /// </summary>
    public class ConnectionEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionEventArgs"/> class using the specified <see cref="Connection"/>.
        /// </summary>
        /// <param name="connection">The <see cref="FrameworkElement.DataContext"/> of a related <see cref="BaseConnection"/>.</param>
        public ConnectionEventArgs(object connection)
            => Connection = connection;

        /// <summary>
        /// Gets or sets the location where the connection should be split.
        /// </summary>
        public Point SplitLocation { get; set; }

        /// <summary>
        /// Gets the <see cref="FrameworkElement.DataContext"/> of the <see cref="BaseConnection"/> associated with this event.
        /// </summary>
        public object Connection { get; }
    }
}
