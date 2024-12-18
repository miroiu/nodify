using System;
using System.Collections.Generic;
using System.Windows;

namespace Nodify.Events
{
    /// <summary>
    /// Represents a method signature used to handle the <see cref="NodifyEditor.ItemsMovedEvent"/> routed event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data containing information about the moved items and their offset.</param>
    public delegate void ItemsMovedEventHandler(object sender, ItemsMovedEventArgs e);

    /// <summary>
    /// Provides data for the <see cref="NodifyEditor.ItemsMovedEvent"/> routed event.
    /// </summary>
    public class ItemsMovedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsMovedEventArgs"/> class with the specified moved items and offset.
        /// </summary>
        /// <param name="items">The collection of items that were moved.</param>
        /// <param name="offset">The vector representing the distance the items were moved.</param>
        public ItemsMovedEventArgs(IReadOnlyCollection<object> items, Vector offset)
        {
            Items = items;
            Offset = offset;
        }

        /// <summary>
        /// Gets or sets the vector representing the distance the items were moved.
        /// </summary>
        public Vector Offset { get; set; }

        /// <summary>
        /// Gets a collection of <see cref="FrameworkElement.DataContext"/>s of the <see cref="ItemContainer"/>s associated with this event.
        /// </summary>
        public IReadOnlyCollection<object> Items { get; }

        protected override void InvokeEventHandler(Delegate genericHandler, object genericTarget)
            => ((ItemsMovedEventHandler)genericHandler)(genericTarget, this);
    }
}
