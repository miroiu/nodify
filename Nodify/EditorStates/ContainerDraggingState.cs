using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>Dragging state of the container.</summary>
    internal sealed class ContainerDraggingState : InputElementStateStack<ItemContainer>.DragState
    {
        protected override bool CanCancel => NodifyEditor.AllowDraggingCancellation;

        private Point _previousMousePosition;

        /// <summary>Constructs an instance of the <see cref="ContainerDraggingState"/> state.</summary>
        /// <param name="container">The owner of the state.</param>
        public ContainerDraggingState(InputElementStateStack<ItemContainer> stack)
            : base(stack, EditorGestures.Mappings.ItemContainer.Drag, EditorGestures.Mappings.ItemContainer.CancelAction)
        {
            PositionElement = Element.Editor;
        }

        protected override void OnBegin(InputElementStateStack<ItemContainer>.InputElementState? from)
        {
            _previousMousePosition = Element.Editor.MouseLocation;
            Element.BeginDragging();
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Element.UpdateDragging(Element.Editor.MouseLocation - _previousMousePosition);
            _previousMousePosition = Element.Editor.MouseLocation;
        }

        protected override void OnEnd(InputEventArgs e)
            => Element.EndDragging();

        protected override void OnCancel(InputEventArgs e)
            => Element.CancelDragging();
    }
}
