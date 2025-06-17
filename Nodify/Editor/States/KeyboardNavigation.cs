using System;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class EditorState
    {
        /// <summary>
        /// Represents the keyboard navigation state of the <see cref="NodifyEditor"/>, allowing users to navigate and interact with nodes and connections using the keyboard.
        /// </summary>
        public class KeyboardNavigation : InputElementState<NodifyEditor>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="KeyboardNavigation"/> class.
            /// </summary>
            /// <param name="element">The <see cref="NodifyEditor"/> associated with this state.</param>
            public KeyboardNavigation(NodifyEditor element) : base(element)
            {
            }

            protected override void OnKeyDown(KeyEventArgs e)
            {
                if (!Element.IsKeyboardFocusWithin || !(e.OriginalSource is DependencyObject originalSource))
                {
                    return;
                }

                double navigationStepSize = GetNavigationStepSize();
                var gestures = EditorGestures.Mappings.Editor.Keyboard;

                if (e.Key == Key.Tab && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    var parentContainer = originalSource.GetParent(Element.IsNavigationTrigger) as UIElement;
                    e.Handled = parentContainer?.Focus() is true;
                }
                else if (Element.IsNavigationTrigger(originalSource))
                {
                    if (gestures.Pan.TryGetNavigationDirection(e, out var panDirection))
                    {
                        var panning = new Vector(-panDirection.X * navigationStepSize, panDirection.Y * navigationStepSize);
                        Element.UpdatePanning(panning);
                        e.Handled = true;
                    }
                    else if (CanDragSelection() && gestures.DragSelection.TryGetNavigationDirection(e, out var dragDirection))
                    {
                        var dragging = new Vector(dragDirection.X * navigationStepSize, -dragDirection.Y * navigationStepSize);
                        Element.BeginDragging();
                        Element.UpdateDragging(dragging);
                        Element.EndDragging();

                        if (NodifyEditor.PanViewportOnKeyboardDrag)
                        {
                            var panning = new Vector(-dragDirection.X * navigationStepSize, dragDirection.Y * navigationStepSize);
                            Element.UpdatePanning(panning);
                        }

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
                        if (NodifyEditor.AutoPanOnNodeFocus)
                        {
                            Element.BringIntoView(itemContainer.Bounds, NodifyEditor.BringIntoViewEdgeOffset);
                        }
                    }
                    else if (Keyboard.FocusedElement is ConnectionContainer connectionContainer)
                    {
                        connectionContainer.Select(SelectionType.Invert);
                        if (NodifyEditor.AutoPanOnNodeFocus)
                        {
                            Element.BringIntoView(connectionContainer.Bounds, NodifyEditor.BringIntoViewEdgeOffset);
                        }
                    }

                    e.Handled = true;
                }
                else if (gestures.DeselectAll.Matches(e.Source, e))
                {
                    if (Element.SelectedContainersCount > 0 && Element.ActiveNavigationLayer?.Id == KeyboardNavigationLayerId.Nodes)
                    {
                        Element.UnselectAll();
                        e.Handled = true;
                    }
                    // TODO: How to get the selected connections count without a hard reference to the connections multi selector?
                    // This currently assumes we have a binding to the SelectedConnectionsProperty dependency property
                    else if (Element.SelectedConnections?.Count > 0 && Element.ActiveNavigationLayer?.Id == KeyboardNavigationLayerId.Connections)
                    {
                        Element.UnselectAllConnections();
                        e.Handled = true;
                    }
                }
                else if (gestures.NextNavigationLayer.Matches(e.Source, e))
                {
                    Element.ActivateNextNavigationLayer();
                    e.Handled = true;
                }
                else if (gestures.PrevNavigationLayer.Matches(e.Source, e))
                {
                    Element.ActivatePreviousNavigationLayer();
                    e.Handled = true;
                }
                else if (Keyboard.FocusedElement is ItemContainer { IsSelected: true } container
                    && EditorGestures.Mappings.GroupingNode.ToggleContentSelection.Matches(e.Source, e))
                {
                    var groupingNode = container.GetChildOfType<GroupingNode>();
                    if (groupingNode != null)
                    {
                        groupingNode.ToggleContentSelection();
                        e.Handled = true;
                    }
                }
            }

            private bool CanDragSelection()
            {
                return Element.ActiveNavigationLayer?.Id == KeyboardNavigationLayerId.Nodes && Element.SelectedContainersCount > 0;
            }

            private double GetNavigationStepSize()
            {
                double cellSize = Element.GridCellSize;

                if (cellSize >= NodifyEditor.MinimumNavigationStepSize)
                    return cellSize;

                int factor = (int)Math.Ceiling(NodifyEditor.MinimumNavigationStepSize / cellSize);
                return factor * cellSize / Element.ViewportZoom;
            }
        }
    }
}
