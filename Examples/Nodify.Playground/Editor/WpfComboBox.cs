using System;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.VisualTree;

namespace Nodify.Playground;

/// <summary>
/// WPF's ComboBox opens popup on pointer pressed.
/// Avalonia's ComboBox opens popup on pointer released.
/// Avalonia behavior interferes with NodeInput which handles pointer pressed to start dragging.
/// In order to keep the same behavior as WPF (only in the demo!), we need to override the pointer pressed and released events
/// so that the popup is opened on pointer pressed.
/// </summary>
public class WpfComboBox : ComboBox
{
    protected override Type StyleKeyOverride => typeof(ComboBox);

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        if (!e.Handled)
        {
            SetCurrentValue(IsDropDownOpenProperty, !IsDropDownOpen);
            e.Handled = true;
        }
    }
    
    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        if ((e.Source as Control)?.GetVisualRoot() is not PopupRoot)
            e.Handled = true;
        base.OnPointerReleased(e);
    }
}