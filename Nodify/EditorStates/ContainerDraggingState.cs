using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>Dragging state of the container.</summary>
    public class ContainerDraggingState : ContainerState
    {
        private Point _initialMousePosition;
        private Point _previousMousePosition;
        private Point _currentMousePosition;
        public bool Canceled { get; set; } = ItemContainer.AllowDraggingCancellation;   // Because of LostMouseCapture that calls Exit

        /// <summary>Constructs an instance of the <see cref="ContainerDraggingState"/> state.</summary>
        /// <param name="container">The owner of the state.</param>
        public ContainerDraggingState(ItemContainer container) : base(container)
        {
        }

        /// <inheritdoc />
        public override void Enter(ContainerState? from, MouseEventArgs? e)
        {
            _initialMousePosition = e?.GetPosition(Editor.ItemsHost) ?? default;

            Container.IsSelected = true;
            Container.IsPreviewingLocation = true;
            Container.RaiseEvent(new DragStartedEventArgs(_initialMousePosition.X, _initialMousePosition.Y)
            {
                RoutedEvent = ItemContainer.DragStartedEvent
            });

            _previousMousePosition = _initialMousePosition;
        }

        /// <inheritdoc />
        public override void Exit()
        {
            Container.IsPreviewingLocation = false;
            var delta = _currentMousePosition - _initialMousePosition;
            Container.RaiseEvent(new DragCompletedEventArgs(delta.X, delta.Y, Canceled)
            {
                RoutedEvent = ItemContainer.DragCompletedEvent
            });
        }

        /// <inheritdoc />
        public override void HandleMouseMove(MouseEventArgs e)
        {
            _currentMousePosition = e.GetPosition(Editor.ItemsHost);
            var delta = _currentMousePosition - _previousMousePosition;
            Container.RaiseEvent(new DragDeltaEventArgs(delta.X, delta.Y)
            {
                RoutedEvent = ItemContainer.DragDeltaEvent
            });

            _previousMousePosition = _currentMousePosition;
        }

        /// <inheritdoc />
        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            EditorGestures.ItemContainerGestures gestures = EditorGestures.Mappings.ItemContainer;

            bool canCancel = gestures.CancelAction.Matches(e.Source, e) && ItemContainer.AllowDraggingCancellation;
            bool canComplete = gestures.Drag.Matches(e.Source, e);
            if (canCancel || canComplete)
            {
                // Prevent canceling if drag and cancel are bound to the same mouse action
                Canceled = !canComplete && canCancel;

                // Handle right click if dragging or canceled and moved the mouse more than threshold so context menus don't open
                if (e.ChangedButton == MouseButton.Right)
                {
                    double contextMenuTreshold = NodifyEditor.HandleRightClickAfterPanningThreshold * NodifyEditor.HandleRightClickAfterPanningThreshold;
                    if ((_currentMousePosition - _initialMousePosition).LengthSquared() > contextMenuTreshold)
                    {
                        e.Handled = true;
                    }
                }

                PopState();
            }
        }

        /// <inheritdoc />
        public override void HandleKeyUp(KeyEventArgs e)
        {
            Canceled = EditorGestures.Mappings.ItemContainer.CancelAction.Matches(e.Source, e) && ItemContainer.AllowDraggingCancellation;
            if (Canceled)
            {
                PopState();
            }
        }
    }
}
