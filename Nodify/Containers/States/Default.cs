using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class ContainerState
    {
        /// <summary>The default state of the <see cref="ItemContainer"/>.</summary>
        public sealed class Default : InputElementStateStack<ItemContainer>
        {
            public Default(ItemContainer container) : base(container)
            {
                PushState(new SelectingState(this));
            }

            private sealed class SelectingState : InputElementState
            {
                private Point _initialPosition;
                private SelectionType? _selectionType;
                private bool _isDragging;

                private bool PreserveSelectionOnRightClick => Element.HasContextMenu || ItemContainer.PreserveSelectionOnRightClick;

                /// <summary>Creates a new instance of the <see cref="ContainerSelectingState"/>.</summary>
                /// <param name="container">The owner of the state.</param>
                public SelectingState(InputElementStateStack<ItemContainer> stack) : base(stack)
                {
                }

                /// <inheritdoc />
                public override void Enter(IInputElementState? from)
                {
                    _isDragging = false;
                    _selectionType = null;
                    _initialPosition = Element.Editor.MouseLocation;
                }

                protected override void OnMouseDown(MouseButtonEventArgs e)
                {
                    if (!Element.IsSelectableLocation(e.GetPosition(Element)))
                    {
                        return;
                    }

                    EditorGestures.ItemContainerGestures gestures = EditorGestures.Mappings.ItemContainer;
                    if (gestures.Drag.Matches(e.Source, e))
                    {
                        // Dragging requires mouse capture
                        _isDragging = Element.IsDraggable && CanCaptureMouse();
                    }

                    if (gestures.Selection.Select.Matches(e.Source, e))
                    {
                        _selectionType = gestures.Selection.GetSelectionType(e);
                    }
                    // Replaces the current selection when right-clicking on an element that has a context menu and is not selected.
                    // Applies only when the select gesture is not right click.
                    else if (e.ChangedButton == MouseButton.Right && PreserveSelectionOnRightClick)
                    {
                        _selectionType = Element.IsSelected ? SelectionType.Append : SelectionType.Replace;
                    }

                    _initialPosition = Element.Editor.MouseLocation;

                    if (_isDragging || _selectionType.HasValue)
                    {
                        e.Handled = true;
                        CaptureMouseSafe();
                    }
                }

                /// <inheritdoc />
                protected override void OnMouseMove(MouseEventArgs e)
                {
                    double dragThreshold = NodifyEditor.MouseActionSuppressionThreshold * NodifyEditor.MouseActionSuppressionThreshold;
                    double dragDistance = (Element.Editor.MouseLocation - _initialPosition).LengthSquared;

                    if (_isDragging && (dragDistance > dragThreshold))
                    {
                        if (!Element.IsSelected)
                        {
                            var selectionType = GetSelectionTypeForDragging(_selectionType);
                            Element.Select(selectionType);
                        }

                        PushState(new Dragging(Stack));
                    }
                }

                /// <inheritdoc />
                protected override void OnMouseUp(MouseButtonEventArgs e)
                {
                    if (_selectionType.HasValue)
                    {
                        // Determine whether the current selection should remain intact or be replaced by the clicked item. 
                        // If the right mouse button is pressed on an already selected item, and the item either has an 
                        // explicit context menu or is configured to preserve the selection on right-click, the selection 
                        // remains unchanged. This ensures that the context menu applies to the entire selection rather 
                        // than only the clicked item.
                        bool allowContextMenu = e.ChangedButton == MouseButton.Right && Element.IsSelected && PreserveSelectionOnRightClick;
                        if (!allowContextMenu)
                        {
                            Element.Select(_selectionType.Value);
                        }
                    }

                    _isDragging = false;
                    _selectionType = null;
                }

                private void CaptureMouseSafe()
                {
                    // Avoid stealing mouse capture from other elements
                    if (CanCaptureMouse())
                    {
                        Element.Focus();
                        Element.CaptureMouse();
                    }
                }

                private bool CanCaptureMouse()
                    => Mouse.Captured == null || Element.IsMouseCaptured;

                private static SelectionType GetSelectionTypeForDragging(SelectionType? selectionType)
                {
                    // Always select the container when dragging
                    return selectionType == SelectionType.Remove
                        ? SelectionType.Replace
                        : selectionType.GetValueOrDefault(SelectionType.Replace);
                }
            }
        }
    }
}
