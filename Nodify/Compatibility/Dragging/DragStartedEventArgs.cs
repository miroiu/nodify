using System;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Nodify.Compatibility;

/// <summary>
/// This DragStartedEventArgs class contains additional information about the
/// DragStarted event.
/// </summary>
/// <seealso cref="Thumb.DragStartedEvent" />
/// <seealso cref="RoutedEventArgs" />
public class DragStartedEventArgs : RoutedEventArgs
{
    /// <summary>
    /// This is an instance constructor for the DragStartedEventArgs class.  It
    /// is constructed with a reference to the event being raised.
    /// </summary>
    /// <returns>Nothing.</returns>
    public DragStartedEventArgs(double horizontalOffset, double verticalOffset) : base()
    {
        _horizontalOffset = horizontalOffset;
        _verticalOffset = verticalOffset;
        RoutedEvent=Thumb.DragStartedEvent;
    }

    /// <value>
    /// Read-only access to the horizontal offset (relative to Thumb's co-ordinate).
    /// </value>
    public double HorizontalOffset
    {
        get { return _horizontalOffset; }
    }

    /// <value>
    /// Read-only access to the vertical offset (relative to Thumb's co-ordinate).
    /// </value>
    public double VerticalOffset
    {
        get { return _verticalOffset; }
    }
        
    private double _horizontalOffset;
    private double _verticalOffset;
}

/// <summary>
///     This delegate must used by handlers of the DragStarted event.
/// </summary>
/// <param name="sender">The current element along the event's route.</param>
/// <param name="e">The event arguments containing additional information about the event.</param>
/// <returns>Nothing.</returns>
public delegate void DragStartedEventHandler(object sender, DragStartedEventArgs e);