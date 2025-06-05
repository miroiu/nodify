using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Nodify.Interactivity
{
    /// <summary>
    /// Manages a stack of input states for a UI element, enabling complex input interactions.
    /// </summary>
    /// <typeparam name="TElement">The type of the associated FrameworkElement.</typeparam>
    public partial class InputElementStateStack<TElement> : IInputHandler
        where TElement : FrameworkElement
    {
        private readonly Stack<IInputElementState> _states = new Stack<IInputElementState>();

        /// <summary>
        /// Gets the associated element for which this state stack is managing input states.
        /// </summary>
        protected TElement Element { get; }

        public bool RequiresInputCapture => State.RequiresInputCapture;

        public bool ProcessHandledEvents => State.ProcessHandledEvents;

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
    }
}
