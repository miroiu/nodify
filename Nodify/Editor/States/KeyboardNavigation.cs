using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace Nodify.Interactivity
{
    public static partial class EditorState
    {
        public class KeyboardNavigation : InputElementState<NodifyEditor>
        {
            private IKeyboardNavigationLayer? ActiveKeyboardNavigationLayer => ((IKeyboardNavigationLayerGroup)Element).ActiveLayer;

            public KeyboardNavigation(NodifyEditor element) : base(element)
            {
            }

            // TODO: If focus is within, do not allow escaping focus trap unless the escape gesture is performed. (some keys like Space or System Keys could try to escape)
            protected override void OnKeyDown(KeyEventArgs e)
            {
                double cellSize = Element.GridCellSize;
                var gestures = EditorGestures.Mappings.Editor.Keyboard;

                if (Element.IsKeyboardFocusWithin && IsEditorControl(e.OriginalSource))
                {
                    if (gestures.Pan.TryGetNavigationDirection(e, out var panDirection))
                    {
                        var panning = new Vector(-panDirection.X * cellSize, panDirection.Y * cellSize);
                        Element.UpdatePanning(panning);
                        e.Handled = true;
                    }
                    else if (CanDragSelection() && gestures.DragSelection.TryGetNavigationDirection(e, out var dragDirection))
                    {
                        var dragging = new Vector(dragDirection.X * cellSize, -dragDirection.Y * cellSize);
                        Element.BeginDragging();
                        Element.UpdateDragging(dragging);
                        Element.EndDragging();

                        // TODO: Find a way to keep the selection in view

                        e.Handled = true;
                    }
                    else if (gestures.NavigateSelection.TryGetFocusDirection(e, out var direction))
                    {
                        Element.MoveFocus(direction);
                        e.Handled = true;
                    }
                }
            }

            protected override void OnKeyUp(KeyEventArgs e)
            {
                var gestures = EditorGestures.Mappings.Editor.Keyboard;

                if (gestures.ToggleSelected.Matches(e.Source, e))
                {
                    if (Keyboard.FocusedElement is ItemContainer itemContainer)
                    {
                        itemContainer.Select(SelectionType.Invert);
                    }
                    else if (Keyboard.FocusedElement is ConnectionContainer connectionContainer)
                    {
                        connectionContainer.Select(SelectionType.Invert);
                    }

                    e.Handled = true;
                }
                else if (gestures.DeselectAll.Matches(e.Source, e))
                {
                    if (Element.SelectedContainersCount > 0 && ActiveKeyboardNavigationLayer?.Id == KeyboardNavigationLayerId.Nodes)
                    {
                        Element.UnselectAll();
                        e.Handled = true;
                    }
                    // TODO: How to get the selected connections count without a hard reference to the connections multi selector?
                    else if (Element.SelectedConnections?.Count > 0 && ActiveKeyboardNavigationLayer?.Id == KeyboardNavigationLayerId.Connections)
                    {
                        Element.UnselectAllConnections();
                        e.Handled = true;
                    }
                }
                else if (gestures.NextNavigationLayer.Matches(e.Source, e))
                {
                    ((IKeyboardNavigationLayerGroup)Element).MoveToNextLayer();
                    e.Handled = true;
                }
                else if (gestures.PrevNavigationLayer.Matches(e.Source, e))
                {
                    ((IKeyboardNavigationLayerGroup)Element).MoveToPrevLayer();
                    e.Handled = true;
                }
            }

            private bool CanDragSelection()
            {
                return ActiveKeyboardNavigationLayer?.Id == KeyboardNavigationLayerId.Nodes && Element.SelectedContainersCount > 0;
            }

            // TODO: Allow for extensibility because connections can be custom
            private static bool IsEditorControl(object originalSource)
            {
                return originalSource is NodifyEditor || originalSource is ItemContainer || originalSource is Connector || originalSource is BaseConnection;
            }
        }
    }
}
