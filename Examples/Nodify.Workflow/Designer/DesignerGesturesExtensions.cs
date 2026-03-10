using Nodify.Interactivity;
using System.Windows.Input;

namespace Nodify.Workflow.Designer;

internal static class DesignerGesturesExtensions
{
    public static void LockEditing(this EditorGestures gestures)
    {
        // Make editor read-only by unbinding all gestures that would allow modifying the workflow

        gestures.Editor.Selection.Unbind();
        gestures.Editor.SelectAll.Unbind();
        gestures.Editor.Cutting.Unbind();
        gestures.Editor.PushItems.Unbind();

        gestures.Editor.Keyboard.ToggleSelected.Unbind();
        gestures.Editor.Keyboard.DragSelection.Unbind();
        gestures.Editor.Keyboard.DeselectAll.Unbind();

        gestures.ItemContainer.Selection.Unbind();
        gestures.ItemContainer.Drag.Unbind();

        gestures.Connection.Disconnect.Unbind();
        gestures.Connection.Split.Unbind();
        gestures.Connection.Selection.Unbind();

        gestures.Connector.Connect.Unbind();
        gestures.Connector.Disconnect.Unbind();

        gestures.Editor.Pan.Value = new AnyGesture(
            new Interactivity.MouseGesture(MouseAction.LeftClick),
            new Interactivity.MouseGesture(MouseAction.RightClick),
            new Interactivity.MouseGesture(MouseAction.MiddleClick));
    }
}
