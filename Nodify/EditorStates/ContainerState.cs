using System.Windows.Input;

namespace Nodify
{
    /// <summary>The base class for container states.</summary>
    public abstract class ContainerState
    {
        /// <summary>Constructs a new <see cref="ContainerState"/>.</summary>
        /// <param name="container">The owner of the state.</param>
        public ContainerState(ItemContainer container)
        {
            Container = container;
        }

        /// <summary>The owner of the state.</summary>
        protected ItemContainer Container { get; }

        /// <summary>The owner of the state.</summary>
        protected NodifyEditor Editor => Container.Editor;

        /// <inheritdoc cref="ItemContainer.OnMouseDown(MouseButtonEventArgs)"/>
        public virtual void HandleMouseDown(MouseButtonEventArgs e) { }

        /// <inheritdoc cref="ItemContainer.OnMouseDown(MouseButtonEventArgs)"/>
        public virtual void HandleMouseUp(MouseButtonEventArgs e) { }

        /// <inheritdoc cref="ItemContainer.OnMouseMove(MouseEventArgs)"/>
        public virtual void HandleMouseMove(MouseEventArgs e) { }

        /// <inheritdoc cref="ItemContainer.OnMouseWheel(MouseWheelEventArgs)"/>
        public virtual void HandleMouseWheel(MouseWheelEventArgs e) { }

        /// <inheritdoc cref="ItemContainer.OnKeyUp(KeyEventArgs)"/>
        public virtual void HandleKeyUp(KeyEventArgs e) { }

        /// <inheritdoc cref="ItemContainer.OnKeyDown(KeyEventArgs)"/>
        public virtual void HandleKeyDown(KeyEventArgs e) { }

        /// <summary>Called when <see cref="ItemContainer.PushState(ContainerState)"/> or <see cref="ItemContainer.PopState"/> is called.</summary>
        /// <param name="from">The state we enter from (is null for root state).</param>
        public virtual void Enter(ContainerState? from, MouseEventArgs? e) { }

        /// <summary>Called when <see cref="ItemContainer.PopState"/> is called.</summary>
        public virtual void Exit() { }

        /// <summary>Called when <see cref="ItemContainer.PopState"/> is called.</summary>
        /// <param name="from">The state we re-enter from.</param>
        public virtual void ReEnter(ContainerState from) { }

        /// <summary>Pushes a new state into the stack.</summary>
        /// <param name="newState">The new state.</param>
        /// <param name="pointerEventArgs"></param>
        public virtual void PushState(ContainerState newState, MouseEventArgs e) => Container.PushState(newState, e);

        /// <summary>Pops the current state from the stack.</summary>
        public virtual void PopState() => Container.PopState();
    }
}
