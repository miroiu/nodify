using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>The default state of the <see cref="ItemContainer"/>.</summary>
    public class ContainerDefaultState : InputElementStateStack<ItemContainer>
    {
        public ContainerDefaultState(ItemContainer container) : base(container)
        {
            PushState(new Implementation(this));
        }

        private sealed class Implementation : InputElementState
        {
            private Point _initialPosition;
            private SelectionType? _selectionType;
            private bool _isDragging;

            /// <summary>Creates a new instance of the <see cref="ContainerSelectingState"/>.</summary>
            /// <param name="container">The owner of the state.</param>
            public Implementation(InputElementStateStack<ItemContainer> stack) : base(stack)
            {
            }

            /// <inheritdoc />
            public override void Enter(InputElementState? from)
            {
                _isDragging = false;
                _selectionType = null;
                _initialPosition = Element.Editor.MouseLocation;
            }

            protected override void OnMouseDown(MouseButtonEventArgs e)
            {
                if (!IsSelectable(e))
                {
                    return;
                }

                EditorGestures.ItemContainerGestures gestures = EditorGestures.Mappings.ItemContainer;
                if (gestures.Drag.Matches(e.Source, e))
                {
                    _isDragging = Element.IsDraggable;
                }

                if (gestures.Selection.Select.Matches(e.Source, e))
                {
                    _selectionType = gestures.Selection.GetSelectionType(e);
                }

                _initialPosition = Element.Editor.MouseLocation;

                // Capture the mouse only if we have an operation
                if (_isDragging || _selectionType.HasValue)
                {
                    Element.Focus();
                    Element.CaptureMouse();
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

                    PushState(new ContainerDraggingState(Stack));
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
                    bool hasContextMenu = Element.HasContextMenu || ItemContainer.PreserveSelectionOnRightClick;
                    bool allowContextMenu = e.ChangedButton == MouseButton.Right && Element.IsSelected && hasContextMenu;
                    if (!(_selectionType == SelectionType.Replace && allowContextMenu))
                    {
                        Element.Select(_selectionType.Value);
                    }
                }

                _isDragging = false;
                _selectionType = null;
            }

            private static SelectionType GetSelectionTypeForDragging(SelectionType? selectionType)
            {
                // Always select the container when dragging
                return selectionType == SelectionType.Remove
                    ? SelectionType.Replace
                    : selectionType.GetValueOrDefault(SelectionType.Replace);
            }

            private bool IsSelectable(MouseButtonEventArgs e)
            {
                if (!Element.IsSelectableLocation(e.GetPosition(Element)))
                {
                    return false;
                }

                if (Mouse.Captured != null && !Element.IsMouseCaptured)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
