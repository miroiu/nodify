using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    /// <summary>
    /// Manages a stack of input states for a UI element, enabling complex input interactions.
    /// </summary>
    /// <typeparam name="TElement">The type of the associated FrameworkElement.</typeparam>
    public class InputElementStateStack<TElement> : IInputHandler
        where TElement : FrameworkElement
    {
        private readonly Stack<IInputElementState> _states = new Stack<IInputElementState>();

        /// <summary>
        /// Gets the associated element for which this state stack is managing input states.
        /// </summary>
        protected TElement Element { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputElementStateStack{TElement}"/> class.
        /// </summary>
        /// <param name="element">The element associated with this state stack.</param>
        public InputElementStateStack(TElement element)
        {
            Element = element;
        }

        /// <summary>
        /// Gets the current state at the top of the stack.
        /// </summary>
        public IInputElementState State => _states.Peek();

        /// <summary>Pushes a new state into the stack.</summary>
        /// <param name="newState">The new state.</param>
        /// <remarks>Calls <see cref="IInputElementState.Enter"/> on the new state.</remarks>
        public void PushState(IInputElementState newState)
        {
            var prev = _states.Count > 0 ? State : null;
            _states.Push(newState);
            newState.Enter(prev);
        }

        /// <summary>Pops the current state from the stack.</summary>
        /// <remarks>It doesn't pop the initial state.
        /// <br />Calls <see cref="IInputElementState.Exit"/> on the current state.
        /// <br />Calls <see cref="IInputElementState.Enter"/> on the new state.</remarks>
        public void PopState()
        {
            // Never remove the default state
            if (_states.Count > 1)
            {
                IInputElementState prev = _states.Pop();
                prev.Exit();
                State.Enter(prev);
            }
        }

        /// <summary>Pops all states from the stack.</summary>
        /// <remarks>It doesn't pop the initial state.
        /// <br />Calls <see cref="IInputElementState.Exit"/> on the current state.
        /// <br />Calls <see cref="IInputElementState.Enter"/> on the previous state.
        /// </remarks>
        public void PopAllStates()
        {
            while (_states.Count > 1)
            {
                PopState();
            }
        }

        public void HandleEvent(InputEventArgs e)
        {
            State.HandleEvent(e);

            if (e.RoutedEvent == UIElement.LostMouseCaptureEvent)
            {
                PopAllStates();
            }
        }

        /// <summary>
        /// Interface representing a state in the input state stack.
        /// </summary>
        public interface IInputElementState : IInputHandler
        {
            /// <summary>
            /// Invoked when entering this state from another state.
            /// </summary>
            /// <param name="from">The state being exited, or null if entering from no prior state.</param>
            void Enter(IInputElementState? from);

            /// <summary>
            /// Invoked when exiting this state.
            /// </summary>
            void Exit();
        }

        /// <summary>
        /// Base class for defining input element states.
        /// </summary>
        public abstract class InputElementState : InputElementState<TElement>, IInputElementState
        {
            /// <summary>
            /// Gets the state stack managing this state.
            /// </summary>
            protected InputElementStateStack<TElement> Stack { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="InputElementState"/> class.
            /// </summary>
            /// <param name="stack">The state stack managing this state.</param>
            public InputElementState(InputElementStateStack<TElement> stack) : base(stack.Element)
            {
                Stack = stack;
            }

            public virtual void Enter(IInputElementState? from) { }

            public virtual void Exit() { }

            /// <summary>
            /// Pushes a new state onto the stack.
            /// </summary>
            /// <param name="newState">The new state to push.</param>
            public void PushState(IInputElementState newState)
                => Stack.PushState(newState);

            /// <summary>
            /// Pops the current state from the stack.
            /// </summary>
            public void PopState()
                => Stack.PopState();
        }

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

                if (_canReceiveInput && (e.RoutedEvent == UIElement.LostMouseCaptureEvent || (CanCancel && CancelGesture?.Matches(e.Source, e) is true && IsInputEventReleased(e))))
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
