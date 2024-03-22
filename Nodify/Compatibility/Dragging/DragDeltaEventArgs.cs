using System;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Nodify.Compatibility;

/// <summary>
/// This DragDeltaEventArgs class contains additional information about the
/// DragDeltaEvent event.
/// </summary>
/// <seealso cref="Thumb.DragDeltaEvent" />
/// <seealso cref="RoutedEventArgs" />
public class DragDeltaEventArgs : RoutedEventArgs
{
    /// <summary>
    /// This is an instance constructor for the DragDeltaEventArgs class.  It
    /// is constructed with a reference to the event being raised.
    /// </summary>
    /// <returns>Nothing.</returns>
    public DragDeltaEventArgs(double horizontalChange, double verticalChange) : base()
    {
        _horizontalChange = horizontalChange;
        _verticalChange = verticalChange;
        RoutedEvent=Thumb.DragDeltaEvent;
    }

    /// <value>
    /// Read-only access to the horizontal change.
    /// </value>
    public double HorizontalChange
    {
        get { return _horizontalChange; }
    }

    /// <value>
    /// Read-only access to the vertical change.
    /// </value>
    public double VerticalChange
    {
        get { return _verticalChange; }
    }

    private double _horizontalChange;
    private double _verticalChange;
}

/// <summary>
///     This delegate must used by handlers of the DragDeltaEvent event.
/// </summary>
/// <param name="sender">The current element along the event's route.</param>
/// <param name="e">The event arguments containing additional information about the event.</param>
/// <returns>Nothing.</returns>
public delegate void DragDeltaEventHandler(object sender, DragDeltaEventArgs e);