using System.Windows;
using System.Windows.Input;

namespace Nodify
{
    public abstract class InputElementState<TState>
        where TState : InputElementState<TState>
    {
        /// <inheritdoc cref="UIElement.OnMouseDown(MouseButtonEventArgs)"/>
        public virtual void HandleMouseDown(MouseButtonEventArgs e) { }

        /// <inheritdoc cref="UIElement.OnMouseUp(MouseButtonEventArgs)"/>
        public virtual void HandleMouseUp(MouseButtonEventArgs e) { }

        /// <inheritdoc cref="UIElement.OnMouseMove(MouseEventArgs)"/>
        public virtual void HandleMouseMove(MouseEventArgs e) { }

        /// <inheritdoc cref="UIElement.OnMouseWheel(MouseWheelEventArgs)"/>
        public virtual void HandleMouseWheel(MouseWheelEventArgs e) { }

        /// <inheritdoc cref="UIElement.OnKeyUp(KeyEventArgs)"/>
        public virtual void HandleKeyUp(KeyEventArgs e) { }

        /// <inheritdoc cref="UIElement.OnKeyDown(KeyEventArgs)"/>
        public virtual void HandleKeyDown(KeyEventArgs e) { }

        /// <param name="from">The state we enter from (is null for root state).</param>
        public virtual void Enter(TState? from) { }

        public virtual void Exit() { }

        /// <param name="from">The state we re-enter from.</param>
        public virtual void ReEnter(TState from) { }
    }
}
