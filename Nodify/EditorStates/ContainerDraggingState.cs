﻿using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>Dragging state of the container.</summary>
    public class ContainerDraggingState : ContainerState
    {
        private Point _initialMousePosition;
        private Point _previousMousePosition;
        
        private bool Canceled { get; set; } = ItemContainer.AllowDraggingCancellation;

        /// <summary>Constructs an instance of the <see cref="ContainerDraggingState"/> state.</summary>
        /// <param name="container">The owner of the state.</param>
        public ContainerDraggingState(ItemContainer container) : base(container)
        {
        }

        /// <inheritdoc />
        public override void Enter(ContainerState? from)
        {
            Canceled = false;

            _initialMousePosition = Editor.MouseLocation;
            _previousMousePosition = _initialMousePosition;

            Container.BeginDragging();
        }

        /// <inheritdoc />
        public override void Exit()
        {
            // TODO: This is not canceled on LostMouseCapture (add OnLostMouseCapture/OnCancel callback?)
            if (Canceled)
            {
                Container.CancelDragging();
            }
            else
            {
                Container.EndDragging();
            }
        }

        /// <inheritdoc />
        public override void HandleMouseMove(MouseEventArgs e)
        {
            Container.UpdateDragging(Editor.MouseLocation - _previousMousePosition);
            _previousMousePosition = Editor.MouseLocation;
        }

        /// <inheritdoc />
        public override void HandleMouseUp(MouseButtonEventArgs e)
        {
            EditorGestures.ItemContainerGestures gestures = EditorGestures.Mappings.ItemContainer;
            if (gestures.Drag.Matches(e.Source, e))
            {
                // Suppress the context menu if the mouse moved beyond the defined drag threshold
                if (e.ChangedButton == MouseButton.Right && Editor.ContextMenu != null)
                {
                    double dragThreshold = NodifyEditor.MouseActionSuppressionThreshold * NodifyEditor.MouseActionSuppressionThreshold;
                    double dragDistance = (Editor.MouseLocation - _initialMousePosition).LengthSquared;

                    if (dragDistance > dragThreshold)
                    {
                        e.Handled = true;
                    }
                }

                PopState();
            }
            else if (ItemContainer.AllowDraggingCancellation && gestures.CancelAction.Matches(e.Source, e))
            {
                Canceled = true;
                e.Handled = true;

                PopState();
            }
        }

        /// <inheritdoc />
        public override void HandleKeyUp(KeyEventArgs e)
        {
            EditorGestures.ItemContainerGestures gestures = EditorGestures.Mappings.ItemContainer;
            if (ItemContainer.AllowDraggingCancellation && gestures.CancelAction.Matches(e.Source, e))
            {
                Canceled = true;
                PopState();
            }
        }
    }
}
