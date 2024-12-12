using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    public partial class InputElementStateStack<TElement> where TElement : FrameworkElement
    {
        /// <summary>
        /// Represents a specialized state for handling drag operations.
        /// </summary>
        public abstract class DragState : InputElementState, IInputHandler
        {
            /// <summary>
            /// The gesture used to exit the drag state.
            /// </summary>
            protected InputGesture ExitGesture { get; }

            /// <summary>
            /// The gesture used to cancel the drag state, if supported.
            /// </summary>
            protected InputGesture? CancelGesture { get; }

            /// <summary>
            /// Gets or sets whether the element has a context menu.
            /// </summary>
            protected virtual bool HasContextMenu => Element.ContextMenu != null;

            /// <summary>
            /// Gets or sets whether the drag operation can be canceled.
            /// </summary>
            protected virtual bool CanCancel { get; } = true;

            /// <summary>
            /// Gets or sets the element used for position calculations.
            /// </summary>
            protected IInputElement PositionElement { get; set; }

            private bool _canReceiveInput;
            private Point _initialPosition;

            /// <summary>
            /// Initializes a new instance of the <see cref="DragState"/> class.
            /// </summary>
            /// <param name="stack">The state stack managing this state.</param>
            /// <param name="exitGesture">The gesture used to exit the drag state.</param>
            public DragState(InputElementStateStack<TElement> stack, InputGesture exitGesture) : base(stack)
            {
                ExitGesture = exitGesture;
                PositionElement = stack.Element;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DragState"/> class with an optional cancel gesture.
            /// </summary>
            /// <param name="stack">The state stack managing this state.</param>
            /// <param name="exitGesture">The gesture used to exit the drag state.</param>
            /// <param name="cancelGesture">The gesture used to cancel the drag state.</param>
            public DragState(InputElementStateStack<TElement> stack, InputGesture exitGesture, InputGesture cancelGesture)
                : this(stack, exitGesture)
            {
                CancelGesture = cancelGesture;
            }

            public sealed override void Enter(IInputElementState? from)
            {
                if (Mouse.Captured == null || Element.IsMouseCaptured)
                {
                    _initialPosition = new Point();
                    _canReceiveInput = true;
                    OnBegin(from);

                    Element.Focus();
                    Element.CaptureMouse();
                }
            }

            public sealed override void Exit()
            {
            }

            void IInputHandler.HandleEvent(InputEventArgs e)
            {
                if (e is MouseEventArgs me && _initialPosition == new Point())
                {
                    _initialPosition = me.GetPosition(PositionElement);
                }

                if (_canReceiveInput && IsInputEventReleased(e) && ExitGesture.Matches(e.Source, e))
                {
                    EndDrag(e);
                    return;
                }

                if (_canReceiveInput && (e.RoutedEvent == UIElement.LostMouseCaptureEvent || CanCancel && CancelGesture?.Matches(e.Source, e) is true && IsInputEventReleased(e)))
                {
                    CancelDrag(e);
                    return;
                }

                if (_canReceiveInput)
                {
                    HandleEvent(e);
                }
            }

            private void CancelDrag(InputEventArgs e)
            {
                _canReceiveInput = false;

                HandleEvent(e);
                OnCancel(e);

                e.Handled = true;

                PopState();
            }

            private void EndDrag(InputEventArgs e)
            {
                _canReceiveInput = false;

                HandleEvent(e);

                // Suppress the context menu if the mouse moved beyond the defined drag threshold
                if (e is MouseButtonEventArgs mbe && mbe.ChangedButton == MouseButton.Right && HasContextMenu)
                {
                    double dragThreshold = NodifyEditor.MouseActionSuppressionThreshold * NodifyEditor.MouseActionSuppressionThreshold;
                    double dragDistance = (mbe.GetPosition(PositionElement) - _initialPosition).LengthSquared;

                    if (dragDistance > dragThreshold)
                    {
                        OnEnd(e);
                        e.Handled = true;
                    }
                    else
                    {
                        OnCancel(e);
                    }
                }
                else
                {
                    OnEnd(e);
                    e.Handled = true;
                }

                PopState();
            }

            /// <summary>
            /// Determines if the given input event represents the release of an input gesture.
            /// </summary>
            /// <param name="e">The input event to evaluate.</param>
            /// <returns>True if the event represents the release of a gesture; otherwise, false.</returns>
            protected virtual bool IsInputEventReleased(InputEventArgs e)
            {
                if (e is MouseButtonEventArgs mbe && mbe.ButtonState == MouseButtonState.Released)
                    return true;

                if (e is KeyEventArgs ke && ke.IsUp)
                    return true;

                if (e is MouseWheelEventArgs mwe && mwe.MiddleButton == MouseButtonState.Released)
                    return true;

                return false;
            }

            /// <summary>
            /// Called when the drag operation begins. Override to provide custom behavior.
            /// </summary>
            /// <param name="e">The input event that started the operation.</param>
            protected virtual void OnBegin(IInputElementState? from)
            {
            }

            /// <summary>
            /// Called when the drag operation ends. Override to provide custom behavior.
            /// </summary>
            /// <param name="e">The input event that ended the operation.</param>
            protected virtual void OnEnd(InputEventArgs e)
            {
            }

            /// <summary>
            /// Called when the drag operation is canceled. Override to provide custom behavior.
            /// </summary>
            /// <param name="e">The input event that canceled the operation.</param>
            protected virtual void OnCancel(InputEventArgs e)
            {
            }
        }
    }
}
