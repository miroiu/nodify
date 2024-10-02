using System;
using System.Windows;

namespace Nodify
{
    /// <summary>
    /// Represents the method that will handle <see cref="NodifyEditor.SelectedConnectionChanged"/> events.
    /// </summary>
    /// <param name="sender">The object where the event handler is attached.</param>
    /// <param name="e">The event data.</param>
    public delegate void ConnectionSelectionEventHandler(object sender, ConnectionSelectionEventArgs e);

    /// <summary>
    /// Provides data for <see cref="BaseConnection"/> selection events.
    /// </summary>
    public class ConnectionSelectionEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionSelectionEventArgs"/> class using the specified <see cref="Connection"/>.
        /// </summary>
        /// <param name="connection">The <see cref="FrameworkElement.DataContext"/> of the selected <see cref="BaseConnection"/>.</param>
        public ConnectionSelectionEventArgs(object connection)
            => Connection = connection;

        /// <summary>
        /// Gets the <see cref="FrameworkElement.DataContext"/> of the <see cref="BaseConnection"/> associated with this event.
        /// </summary>
        public object Connection { get; }

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
            => ((ConnectionSelectionEventHandler)genericHandler)(genericTarget, this);
    }
}
