using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public static partial class ContainerState
    {
        /// <summary>Dragging state of the container.</summary>
        internal sealed class Dragging : InputElementStateStack<ItemContainer>.DragState
        {
            protected override bool CanCancel => NodifyEditor.AllowDraggingCancellation;
            protected override bool IsToggle => EnableToggledDraggingMode;

            private Point _previousMousePosition;

            /// <summary>Constructs an instance of the <see cref="Dragging"/> state.</summary>
            /// <param name="container">The owner of the state.</param>
            public Dragging(InputElementStateStack<ItemContainer> stack)
                : base(stack, EditorGestures.Mappings.ItemContainer.Drag, EditorGestures.Mappings.ItemContainer.CancelAction)
            {
                PositionElement = Element.Editor;
            }

            protected override void OnBegin(InputEventArgs e)
            {
                _previousMousePosition = Element.Editor.MouseLocation;
                Element.BeginDragging();
            }

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
}
