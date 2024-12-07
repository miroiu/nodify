using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public class InputElementStateStack<TElement> : IInputHandler
        where TElement : FrameworkElement
    {
        private readonly Stack<InputElementState> _states = new Stack<InputElementState>();

        protected TElement Element { get; }

        public InputElementStateStack(TElement element)
        {
            Element = element;
        }

        /// <summary>The current element state.</summary>
        public InputElementState State => _states.Peek();

        /// <summary>Pushes a new state into the stack.</summary>
        /// <param name="newState">The new state.</param>
        /// <remarks>Calls <see cref="InputElementState.Enter"/> on the new state.</remarks>
        public void PushState(InputElementState newState)
        {
            var prev = _states.Count > 0 ? State : null;
            _states.Push(newState);
            newState.Enter(prev);
        }

        /// <summary>Pops the current state from the stack.</summary>
        /// <remarks>It doesn't pop the initial state.
        /// <br />Calls <see cref="InputElementState.Exit"/> on the current state.
        /// <br />Calls <see cref="InputElementState.Enter"/> on the new state.</remarks>
        public void PopState()
        {
            // Never remove the default state
            if (_states.Count > 1)
            {
                InputElementState prev = _states.Pop();
                prev.Exit();
                State.Enter(prev);
            }
        }

        /// <summary>Pops all states from the stack.</summary>
        /// <remarks>It doesn't pop the initial state.
        /// <br />Calls <see cref="InputElementState.Exit"/> on the current state.
        /// <br />Calls <see cref="InputElementState.Enter"/> on the previous state.
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
            ((IInputHandler)State).HandleEvent(e);

            if (e.RoutedEvent == UIElement.LostMouseCaptureEvent)
            {
                PopAllStates();
            }
        }

        public abstract class InputElementState : InputElementState<TElement>
        {
            protected InputElementStateStack<TElement> Stack { get; }

            public InputElementState(InputElementStateStack<TElement> stack) : base(stack.Element)
            {
                Stack = stack;
            }

            /// <param name="from">The state we enter from (null for root state).</param>
            public virtual void Enter(InputElementState? from) { }

            public virtual void Exit() { }

            public void PushState(InputElementState newState)
                => Stack.PushState(newState);

            public void PopState()
                => Stack.PopState();
        }

        public abstract class DragState : InputElementState, IInputHandler
        {
            protected InputGesture ExitGesture { get; }
            protected InputGesture? CancelGesture { get; }

            protected virtual bool HasContextMenu => Element.ContextMenu != null;
            protected IInputElement PositionElement { get; set; }

            private bool _canReceiveInput;
            private Point _initialPosition;

            public DragState(InputElementStateStack<TElement> stack, InputGesture exitGesture) : base(stack)
            {
                ExitGesture = exitGesture;
                PositionElement = stack.Element;
            }

            public DragState(InputElementStateStack<TElement> stack, InputGesture exitGesture, InputGesture cancelGesture)
                : this(stack, exitGesture)
            {
                CancelGesture = cancelGesture;
            }

            public sealed override void Enter(InputElementState? from)
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

                if (_canReceiveInput && (e.RoutedEvent == UIElement.LostMouseCaptureEvent || (CancelGesture?.Matches(e.Source, e) is true && IsInputEventReleased(e))))
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

            private static bool IsInputEventReleased(InputEventArgs e)
            {
                if (e is MouseButtonEventArgs mbe && mbe.ButtonState == MouseButtonState.Released)
                    return true;

                if (e is KeyEventArgs ke && ke.IsUp)
                    return true;

                if (e is MouseWheelEventArgs mwe && mwe.MiddleButton == MouseButtonState.Released)
                    return true;

                return false;
            }

            protected virtual void OnBegin(InputElementState? from)
            {
            }

            protected virtual void OnEnd(InputEventArgs e)
            {
            }

            protected virtual void OnCancel(InputEventArgs e)
            {
            }
        }
    }
}
