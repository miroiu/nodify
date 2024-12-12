using System.Windows;

namespace Nodify.Interactivity
{
    public partial class InputElementStateStack<TElement> where TElement : FrameworkElement
    {
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
    }
}
